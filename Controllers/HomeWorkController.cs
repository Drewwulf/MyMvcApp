using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
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

        public IActionResult Details(int id)
        {
            var HomeworkPlace = _context.Homeworks.Find(id);
            var modal = new HomeworkViewModel
            {
                HomeworkName = HomeworkPlace.HomeworkName,
                HomeworkDescription = HomeworkPlace.HomeworkDescription,
                StartTime = HomeworkPlace.StartTime,
                SubmitTime = HomeworkPlace.SubmitTime,
            };

            return View(modal); 
        }

        public IActionResult Info()
        {
            var allStudents = _context.Students.ToList();
            var allHomeWorks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
            var allStudentsToHomeWorks = _context.StudentsToHomeworks.OrderByDescending(s => s.StudentsToHomeworkId).ToList();

            var model = new HomeworkViewModel { homeworks = allHomeWorks, students = allStudents, studentsToHomeworks = allStudentsToHomeWorks };

            return View(model); // повертає Views/HomeWork/Info.cshtml
        }
        [HttpPost]
        public IActionResult Add(HomeworkViewModel model)
        {

            var allSH = _context.StudentsToHomeworks.Any(h => h.StudentId == model.StudentId && h.HomeworkId == model.HomeworkId);
            if (allSH) 
            {
                TempData["Error"] = "Ця домашння вже задана учневі";
                return RedirectToAction("Info");
            }


            var studentToHomeworkRecord = new StudentsToHomework
            {
                HomeworkId = model.HomeworkId,
                StudentId = model.StudentId,
                IsEnded = false
            };
            _context.StudentsToHomeworks.Add(studentToHomeworkRecord);
            _context.SaveChanges();
            return RedirectToAction("Info");

        }
        public IActionResult Create()
        {
             var allHomeworks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
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
 var allHomeworks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
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
 var allHomeworks = _context.Homeworks.OrderByDescending(h => h.HomeworkId).ToList();
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
            homeworkPlace.isdeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Create");
        }
    }
}