using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using Radiostation.DAL.Repositories;
using Radiostation.WebUI.Models;
using X.PagedList;

namespace Radiostation.WebUI.Controllers
{
    public class BroadGridController : Controller
    {
        private readonly IBaseRepository<Employee> _employeeRepository;
        private readonly IBaseRepository<Translation> _translationRepository;
        private readonly IBaseRepository<Track> _trackRepository;

        public BroadGridController(IBaseRepository<Employee> employeeRepository, IBaseRepository<Translation> translationRepository, IBaseRepository<Track> trackRepository)
        {
            _employeeRepository = employeeRepository;
            _translationRepository = translationRepository;
            _trackRepository = trackRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TrackNoTime()
        {
            var tracks = await _trackRepository.GetEntities()
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Tracks = tracks;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrackNoTime(DateTime start, DateTime end)
        {
            var tracks = await _trackRepository.GetEntities()
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Tracks = tracks;

            var result = tracks
                .Where(t => !t.Translations.Any(x => x.Date >= start && x.Date <= end))
                .Select(x => $"{x.Performer.Name} - {x.Name}")
                .ToList();

            return View(result);
        }

        public async Task<IActionResult> TrackTime(int? trackId)
        {
            var tracks = await _trackRepository.GetEntities()
                .Include(t => t.Translations)
                    .ThenInclude(t => t.Employee)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Tracks = tracks;

            if (trackId == null)
            {
                return View();
            }

            var result = tracks
                .First(t => t.Id == trackId).Translations
                .Select(t => new TrackEntryViewModel
                {
                    Date = t.Date,
                    EmployeeName = $"{t.Employee.FirstName} {t.Employee.LastName}"
                })
                .ToList();

            return View(result);
        }

        public async Task<IActionResult> Employee(int? employeeId)
        {
            var employees = await _employeeRepository.GetEntities()
                .Select(t => new Employee
                {
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Id = t.Id
                })
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Employees = employees;

            if (employeeId == null)
            {
                return View();
            }

            var workTimes = await _translationRepository.GetEntities()
                .Where(t => t.EmployeeId == employeeId)
                .AsNoTracking()
                .ToListAsync();

            var groupedEmployees = workTimes
                .GroupBy(t => new
                {
                    t.EmployeeId,
                    Date = new DateTime(t.Date.Year, t.Date.Month, 1)
                })
                .OrderBy(t => t.Key.Date)
                .Select(t =>
                {
                    var employee = t.First().Employee;

                    return new EmployeeWorkTimeViewModel
                    {
                        LastName = employee.LastName,
                        FirstName = employee.FirstName,
                        Education = employee.Education,
                        Role = employee.Role,
                        WorkedHours = t.Sum(x => GetSeconds(x.Track.Duration) / 3600.0),
                        Month = t.Key.Date.Month,
                        Year = t.Key.Date.Year
                    };
                })
                .ToList();

            return View(groupedEmployees);
        }

        public async Task<IActionResult> AllEmployees()
        {
            var currentDate = DateTime.Now;
            var currentOffset = currentDate.DayOfWeek;

            var startOfWeek = currentDate.AddDays(-(int)currentOffset + 1);
            var endOfWeek = currentDate.AddDays(7 - (int)currentOffset);

            startOfWeek = new DateTime(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day, 0, 0, 0);
            endOfWeek = new DateTime(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59);

            var workTimes = await _translationRepository.GetEntities()
                .Where(t => t.Date >= startOfWeek && t.Date <= endOfWeek)
                .AsNoTracking()
                .ToListAsync();

            var groupedEmployees = workTimes
                .GroupBy(t => t.EmployeeId)
                .Select(t =>
                {
                    var employee = t.First().Employee;

                    return new EmployeeWorkTimeViewModel
                    {
                        LastName = employee.LastName,
                        FirstName = employee.FirstName,
                        Education = employee.Education,
                        Role = employee.Role,
                        WorkedHours = t.Sum(x => GetSeconds(x.Track.Duration) / 3600.0)
                    };
                })
                .ToList();

            return View(groupedEmployees);
        }

        private static int GetSeconds(string trackDuration)
        {
            var splatted = trackDuration.Split(":");

            var minutes = int.Parse(splatted[0]);
            var seconds = int.Parse(splatted[1]);

            return minutes * 60 + seconds;
        }
    }
}