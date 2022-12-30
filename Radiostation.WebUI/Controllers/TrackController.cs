using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using X.PagedList;

namespace Radiostation.WebUI.Controllers
{
    public class TrackController : Controller
    {
        private const int PageSize = 10;
        private readonly IBaseRepository<Track> _repository;
        private readonly IBaseRepository<Performer> _performerRepository;
        private readonly IBaseRepository<Genre> _genreRepository;

        public TrackController(IBaseRepository<Track> repository, IBaseRepository<Genre> genreRepository, IBaseRepository<Performer> performerRepository)
        {
            _repository = repository;
            _genreRepository = genreRepository;
            _performerRepository = performerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var query = _repository.GetEntities();
            if (searchString != null)
            {
                query = query
                    .Where(t => t.Name.ToLower().Contains(searchString.ToLower().Trim()))
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

            var genres = await _genreRepository.GetEntities()
                .ToListAsync();
            var performers = await _performerRepository.GetEntities()
                .ToListAsync();

            ViewBag.Genres = genres;
            ViewBag.Performers = performers;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Track item)
        {
            await _repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genres = await _genreRepository.GetEntities()
                .ToListAsync();
            var performers = await _performerRepository.GetEntities()
                .ToListAsync();

            ViewBag.Genres = genres;
            ViewBag.Performers = performers;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Track item)
        {
            await _repository.Create(item);

            return RedirectToAction(nameof(Index));
        }
    }
}