using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
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
  public IActionResult Edit(int id)
        {
            var allDirections = _context.Directions.ToList();
           
            var test =_context.Tests.Find(id);
            var modal = new TestViewModel
            {
                TestId = id,
                Name=test.TestName,
                Description=test.TestDescription,
                difficualtyEnum = test.TestDifficualty,
                TestDifficualty = Enum.GetValues(typeof(TestDifficualtyEnum))
                     .Cast<TestDifficualtyEnum>()
                     .ToList(),
                directions =allDirections

            };
            return View(modal); // повертає Views/Direction/Edit.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TestViewModel test)
        {
        
             var test1 = _context.Tests.Find(test.TestId);
 

    test1.TestName = test.Name;
    test1.TestDescription = test.Description;
    test1.TestDifficualty = test.difficualtyEnum;
    test1.DirectionId=test.DirectionId;

    _context.SaveChanges();

             return RedirectToAction("Edit");  // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Details(int id){

        var Test = _context.Tests.Find(id); 
          var modal = new TestViewModel
          {
            TestId = id,
            Name =Test.TestName,
            Description=Test.TestDescription
          };

            return View(modal); // повертає Views/Destination/Details.cshtml
        
        }
      
        public IActionResult Create()
        {
            var allTest = _context.Tests.OrderByDescending(t => t.TestId).ToList();
            var allDirections = _context.Directions.OrderByDescending(d => d.DirectionId).ToList();
          
            var model = new TestViewModel{ test = allTest,directions = allDirections,TestDifficualty = Enum.GetValues(typeof(TestDifficualtyEnum))
                     .Cast<TestDifficualtyEnum>()
                     .ToList()
            };

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
                TestDifficualty =TestViewModel.difficualtyEnum ,
                Direction = direction
            };
            _context.Tests.Add(test);
            _context.SaveChanges();
            var allTest = _context.Tests.OrderByDescending(t => t.TestId).ToList();
            var model = new TestViewModel{ test = allTest};

            return RedirectToAction("Create"); 


        }
        public IActionResult Delete(int id)
        {
            var Test = _context.Tests.Find(id);
            Test.isdeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}