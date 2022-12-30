using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using X.PagedList;

namespace Radiostation.WebUI.Controllers
{
    public class TranslationController : Controller
    {
        private const int PageSize = 10;
        private readonly IBaseRepository<Track> _trackRepository;
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly IBaseRepository<Translation> _repository;

        public TranslationController(IBaseRepository<Track> trackRepository, IBaseRepository<Translation> repository, IBaseRepository<Employee> employeeRepository)
        {
            _trackRepository = trackRepository;
            _repository = repository;
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var query = _repository.GetEntities();
            if (searchString != null)
            {
                query = query
                    .Where(t => t.Track.Name.ToLower().Contains(searchString.ToLower().Trim()))
                    .Take(PageSize);
            }

            var entities = await query
                .ToListAsync();

            page ??= 1;
            var pagedItems = entities
                .ToPagedList(page.Value, PageSize);

            return View(pagedItems);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _repository.GetEntityById(id);

            var employees = await _employeeRepository.GetEntities()
                .ToListAsync();
            var tracks = await _trackRepository.GetEntities()
                .ToListAsync();

            ViewBag.Tracks = tracks;
            ViewBag.Employees = employees;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Translation item)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeRepository.GetEntities()
                    .ToListAsync();
                var tracks = await _trackRepository.GetEntities()
                    .ToListAsync();

                ViewBag.Tracks = tracks;
                ViewBag.Employees = employees;

                return View(item);
            }

            var times = item.Time.Split(":")
                .Select(int.Parse)
                .ToList();

            item.Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, times[0], times[1], 0);

            await _repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var employees = await _employeeRepository.GetEntities()
                .ToListAsync();
            var tracks = await _trackRepository.GetEntities()
                .ToListAsync();

            ViewBag.Tracks = tracks;
            ViewBag.Employees = employees;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Translation item)
        {
            if (!ModelState.IsValid)
            {
                var employees = await _employeeRepository.GetEntities()
                    .ToListAsync();
                var tracks = await _trackRepository.GetEntities()
                    .ToListAsync();

                ViewBag.Tracks = tracks;
                ViewBag.Employees = employees;

                return View(item);
            }

            var times = item.Time.Split(":")
                .Select(int.Parse)
                .ToList();
            item.Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day, times[0], times[1], 0);

            await _repository.Create(item);

            return RedirectToAction(nameof(Index));
        }
    }
}