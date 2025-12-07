using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class DestinationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestinationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(); // повертає Views/Destination/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Destination/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlaceViewModel placeViewModel)
        {
            var place = new Place
            {
                Name = placeViewModel.Name,
                Address = placeViewModel.Address
            };
            _context.Places.Add(place);
            _context.SaveChanges();

            return View();
        }
    }
}