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
            var allTask = _context.Tasks.ToList();
            var allDirections = _context.Directions.ToList();
            var model = new TaskViewModel { Question = allTask };

            return View(model);
        }
        public IActionResult Create(TaskViewModel TaskViewModel)
        {
            var direction = _context.Directions.Find(TaskViewModel.DirectionId);

            var task = new Question
            {
                QuestionName = TaskViewModel.Name,
                Ansver1Name  = TaskViewModel.Name,
                Ansver2Name = TaskViewModel.Name,
                Ansver3Name = TaskViewModel.Name,
                Ansver4Name = TaskViewModel.Name,
                
            };
            _context.Tasks.Add(task);
            _context.SaveChanges();
            var allTask = _context.Tasks.ToList();
            var model = new TaskViewModel { Question = allTask };

            return RedirectToAction("Create");


        }
    }
}