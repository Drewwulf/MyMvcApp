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
        public IActionResult Edit(int id)
        {
            var directionPlace =_context.Directions.Find(id);
            var modal = new DirectionViewModel
            {
                DirectionId = id,
                DirectionName= directionPlace.DirectionName,
                DirectionDescription=directionPlace.DirectionDescription

            };
            return View(modal); // повертає Views/Direction/Edit.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Edit(DirectionViewModel modal)
        {
        
             var direction = _context.Directions.Find(modal.DirectionId);


    direction.DirectionName = modal.DirectionName;
    direction.DirectionDescription = modal.DirectionDescription;

    _context.SaveChanges();

            return View(modal); // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Details(int id){

        var directionPlace = _context.Directions.Find(id); 
          var modal = new DirectionViewModel
          {
            DirectionName =directionPlace.DirectionName,
            DirectionDescription=directionPlace.DirectionDescription
          };

            return View(modal); // повертає Views/Destination/Details.cshtml
        
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
