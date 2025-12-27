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
             public IActionResult Edit(int id)
        {
            var destinationPlace =_context.Places.Find(id);
            var modal = new PlaceViewModel
            {
                Id = destinationPlace.PlaceId,
                Name= destinationPlace.DestinationName,
                Address=destinationPlace.DestinationAddress,

            };
            return View(modal); // повертає Views/Direction/Edit.cshtml
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlaceViewModel placeViewModel)
        {
            var place = new Place
            {
                DestinationName = placeViewModel.Name,
                DestinationAddress = placeViewModel.Address
            };
            _context.Places.Update(place);
            _context.SaveChanges();
 var allDestinations = _context.Places.ToList();
            var model = new PlaceViewModel{ places = allDestinations};

            return RedirectToAction("Edit"); 
        }
        public IActionResult Details(int id)
        {
          var place = _context.Places.Find(id); 
          var modal = new PlaceViewModel
          {
              Name = place.DestinationName,
              Address = place.DestinationAddress
          };


            return View(modal); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            var allDestinations = _context.Places.ToList();
            var model = new PlaceViewModel{ places = allDestinations};

            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlaceViewModel placeViewModel)
        {
            var place = new Place
            {
                DestinationName = placeViewModel.Name,
                DestinationAddress = placeViewModel.Address
            };
            _context.Places.Add(place);
            _context.SaveChanges();
 var allDestinations = _context.Places.ToList();
            var model = new PlaceViewModel{ places = allDestinations};

            return RedirectToAction("Create"); 
        }

    }
}