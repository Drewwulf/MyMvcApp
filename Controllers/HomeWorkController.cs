using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class HomeWorkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeWorkController(ApplicationDbContext context)
        {
            _context = context;
        } 
    
        public IActionResult Index()
        {
            return View(); // повертає Views/Homework/Index.cshtml
        }

        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
             var allHomeworks = _context.Homeworks.ToList();
            var model = new HomeworkViewModel{ homeworks = allHomeworks};

            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HomeworkViewModel homeworkViewModel)
        {
            var homework = new Homework
            {
                HomeworkName = homeworkViewModel.HomeworkName,
                HomeworkDescription = homeworkViewModel.HomeworkDescription,
                StartTime = homeworkViewModel.StartTime,
                SubmitTime = homeworkViewModel.SubmitTime
            };
            _context.Homeworks.Add(homework);
            _context.SaveChanges();
 var allHomeworks = _context.Homeworks.ToList();
            var model = new HomeworkViewModel{ homeworks = allHomeworks};

            return RedirectToAction("Create"); 
        }


         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeworkViewModel homeworkViewModel)
        {
            var homework = new Homework
            {
                HomeworkName = homeworkViewModel.HomeworkName,
                HomeworkDescription = homeworkViewModel.HomeworkDescription
            };
            _context.Homeworks.Update(homework);
            _context.SaveChanges();
 var allHomeworks = _context.Homeworks.ToList();
            var model = new HomeworkViewModel{ homeworks = allHomeworks};

            return RedirectToAction("Edit"); 
        }
         public IActionResult Edit(int id)
        
        {               
            var homeworkPlace =_context.Homeworks.Find(id);
            var modal = new HomeworkViewModel
            {
                HomeworkId = id,
                HomeworkName= homeworkPlace.HomeworkName,
                HomeworkDescription=homeworkPlace.HomeworkDescription

            };
            return View(modal); // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Delete(int id)
        {
            var homeworkPlace =_context.Homeworks.Find(id);
            _context.Homeworks.Remove(homeworkPlace);
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}