using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            return View(); // повертає Views/Schedule/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Schedule/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create(int groupId)
        {
            var allSchedules = _context.Schedules.ToList();
            var model = new SheduleViewModel { Places = _context.Places.ToList(),
                DaysOfWeek = Enum.GetValues(typeof(WeekDay))
                     .Cast<WeekDay>()
                     .ToList(),StudyGroupId = groupId
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SheduleViewModel SheduleViewModel)
        {
            var model = new Schedule
            {
                PlaceId = SheduleViewModel.PlaceId,
                StudyGroupId = SheduleViewModel.StudyGroupId,
                DayOfWeek = SheduleViewModel.DayOfWeek,
                startTime = SheduleViewModel.Time,
                endTime = SheduleViewModel.EndTime,
            };
            _context.Schedules.Add(model);
            _context.SaveChanges();
            var allDestinations = _context.Places.ToList();
            

            return RedirectToAction("Create");
        }
      
    
}
}