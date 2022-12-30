using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using X.PagedList;

namespace Radiostation.WebUI.Controllers
{
    public class PerformerController : Controller
    {
        private const int PageSize = 10;
        private readonly IBaseRepository<Performer> _repository;

        public PerformerController(IBaseRepository<Performer> repository)
        {
            _repository = repository;
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

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Performer item)
        {
            await _repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Performer item)
        {
            await _repository.Create(item);

            return RedirectToAction(nameof(Index));
        }
    }
}