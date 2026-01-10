using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class TestController : Controller
    {
         private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(); // повертає Views/Test/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Test/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
      
        public IActionResult Create()
        {
            var allTest = _context.Tests.ToList();
            var allDirections = _context.Directions.ToList();
            var model = new TestViewModel{ test = allTest,directions = allDirections};

            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TestViewModel TestViewModel)
        {
            var direction = _context.Directions.Find(TestViewModel.DirectionId);

            var test = new Test
            {
                TestName = TestViewModel.Name,
                TestDescription = TestViewModel.Description,
                TestDifficualty = TestViewModel.Difficualty,
                Direction = direction
            };
            _context.Tests.Add(test);
            _context.SaveChanges();
            var allTest = _context.Tests.ToList();
            var model = new TestViewModel{ test = allTest};

            return RedirectToAction("Create"); 
        }
    }
}