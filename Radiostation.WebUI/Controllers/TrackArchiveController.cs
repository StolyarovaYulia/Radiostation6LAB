using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;

namespace Radiostation.WebUI.Controllers
{
    public class TrackArchiveController : Controller
    {
        private readonly IBaseRepository<Track> _repository;

        public TrackArchiveController(IBaseRepository<Track> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string genre, string performer)
        {
            var query = _repository.GetEntities();

            if (!string.IsNullOrEmpty(genre))
            {
                query = query
                    .Where(t => t.Genre.Name.ToLower().Contains(genre.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(performer))
            {
                query = query
                    .Where(t => t.Performer.Name.ToLower().Contains(performer.Trim().ToLower()));
            }

            var tracks = await query
                .ToListAsync();

            return View(tracks);
        }
    }
}