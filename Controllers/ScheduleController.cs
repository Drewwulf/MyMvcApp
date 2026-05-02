using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using System.Runtime.CompilerServices;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
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
        public IActionResult Create()
        {
            var allSchedules = _context.Schedules.ToList();
            var model = new SheduleViewModel {Places=_context.Places.ToList()};

            return View(model);
        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(SheduleViewModel sheduleViewModel)
//        {
//            var schedule = new Schedule
//            {
//                DestinationName = placeViewModel.Name,
//                DestinationAddress = placeViewModel.Address
//            };
//            _context.Places.Add(place);
//            _context.SaveChanges();
//            var allDestinations = _context.Places.ToList();
//            var model = new SheduleViewModel { };

//            return RedirectToAction("Create");
//        }
//            return View(); // повертає Views/Destination/Details.cshtml
//    }
}
}