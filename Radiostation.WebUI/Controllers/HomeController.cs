using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Radiostation.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using Radiostation.WebUI.Extensions;
using X.PagedList;

namespace Radiostation.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBaseRepository<Translation> _repository;

        public HomeController(IBaseRepository<Translation> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int? offset)
        {
            offset ??= 0;
            var translations = await _repository.GetEntities()
                    .ToListAsync();

            var viewModel = new DashboardViewModel
            {
                YesterdayDate = DateTime.Now.AddDays(-1 + offset.Value),
                Yesterday = translations
                    .Where(t => DateTime.Now.AddDays(-1 + offset.Value).IsEqualDay(t.Date))
                    .ToList(),
                TomorrowDate = DateTime.Now.AddDays(1 + offset.Value),
                Tomorrow = translations
                    .Where(t => DateTime.Now.AddDays(1 + offset.Value).IsEqualDay(t.Date))
                    .ToList(),
                TodayDate = DateTime.Now.AddDays(offset.Value),
                Today = translations
                    .Where(t => DateTime.Now.AddDays(offset.Value).IsEqualDay(t.Date))
                    .ToList()
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
