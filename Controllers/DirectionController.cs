using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class DirectionController : Controller
    { private readonly ApplicationDbContext _context;

        public DirectionController(ApplicationDbContext context)
        {
            _context = context;
        }        public IActionResult Index()
        {
            return View(); // повертає Views/Direction/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
          public IActionResult Create()
        {
            var allDirections = _context.Directions.ToList();
            var model = new DirectionViewModel{directions = allDirections};
            return View(model); // повертає Views/Destination/Details.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectionViewModel directionViewModel)
        {
           var direction = new Direction
           {
               DirectionName = directionViewModel.DirectionName,
               DirectionDescription = directionViewModel.DirectionDescription

           };
_context.Directions.Add(direction);
_context.SaveChanges();
var allDirections = _context.Directions.ToList();
            var model = new DirectionViewModel{directions = allDirections};
            return View(model);
        }
    }
}
