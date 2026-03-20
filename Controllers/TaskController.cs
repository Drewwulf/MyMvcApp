using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(); // повертає Views/Task/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Task/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Tasks(int id)
        {
            var allTask = _context.Tasks.Where(x=>x.TestId==id).ToList();
            var allDirections = _context.Directions.ToList();
            var model = new TaskViewModel { Question = allTask, TestId=id };

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(TaskViewModel TaskViewModel)
        {
           

            var task = new Question
            {
                QuestionName = TaskViewModel.Name,
                QuestionDescription = TaskViewModel.Description,
                Ansver1Name  = TaskViewModel.Ansver1Name,
                Ansver2Name = TaskViewModel.Ansver2Name,
                Ansver3Name = TaskViewModel.Ansver3Name,
                Ansver4Name = TaskViewModel.Ansver4Name,
                TestId = TaskViewModel.TestId
                
            };
            _context.Tasks.Add(task);
            _context.SaveChanges();
            var allTask = _context.Tasks.ToList();
            var model = new TaskViewModel { Question = allTask };

            return RedirectToAction("Tasks");


        }
    }
}