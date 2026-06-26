using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;

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
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            var modal = new TaskViewModel
            {
                TaskId = task.QuestionId,
                Name = task.QuestionName,
                Description = task.QuestionDescription,
                TaskType = task.QuestionType,
            };

            return View(modal); // повертає Views/Task/Edit.cshtml
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

        public IActionResult Edit(TaskViewModel modal)
        {
            var task = _context.Tasks.Find(modal.TaskId);
            var taskCorrectCount = _context.Answers.Where(c => c.QuestionId == modal.TaskId).ToList().Count;

            task.QuestionName = modal.Name;
            task.QuestionDescription = modal.Description;
            if (taskCorrectCount == 1)
            {
                task.QuestionType = modal.TaskType;
            }
            else
            {
                if (modal.TaskType == "Radio")
                {
                    TempData["Error"] = "Неможиво змінити тип на Radio тому що в тесті більеш одної відповіді.";
                    return View(modal);
                }
            }

            _context.SaveChanges();

            return View(modal);
        }

        public IActionResult Create(TaskViewModel TaskViewModel)
        {
           

            var task = new Question
            {
                QuestionName = TaskViewModel.Name,
                QuestionDescription = TaskViewModel.Description,
                QuestionType = TaskViewModel.TaskType,
          
                TestId = TaskViewModel.TestId
                
            };
            _context.Tasks.Add(task);
            _context.SaveChanges();
            var allTask = _context.Tasks.ToList();
            var model = new TaskViewModel { Question = allTask };
            return RedirectToAction("Tasks", new { id = TaskViewModel.TestId });


        }
    }
}