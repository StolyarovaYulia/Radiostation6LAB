using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;

namespace Radiostation.WebUI.Controllers
{
    public class StaffController : Controller
    {
        private readonly IBaseRepository<Employee> _repository;

        public StaffController(IBaseRepository<Employee> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string lastName, string firstName, string education)
        {
            var query = _repository.GetEntities();

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query
                    .Where(x => x.LastName.ToLower().Contains(lastName.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query
                    .Where(x => x.FirstName.ToLower().Contains(firstName.Trim().ToLower()));
            }

            if (!string.IsNullOrEmpty(education))
            {
                query = query
                    .Where(x => x.Education.ToLower().Contains(education.Trim().ToLower()));
            }

            var employees = await query
                .ToListAsync();

            return View(employees);
        }
    }
}