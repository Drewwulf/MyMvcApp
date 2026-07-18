using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Controllers
{
    public class AnswerController : Controller
    {

        private readonly ApplicationDbContext _context;
        private int id;
        private List<Direction> allDirections;
        private object allAnswers;

        public AnswerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public IActionResult Edit(int id)
        {
            var answer = _context.Answers.Find(id);

            var modal = new AnswerViewModel
            {
                AnswerName = answer.answerName,
                AnswerId = answer.Id,
                IsCorrect = answer.IsCorrect,
                TestId = answer.Question.TestId
            };

            return View(modal);
        }
        public IActionResult Details(int id)
        {

            var allAnswer = _context.Answers.Where(x => x.QuestionId == id).ToList();

            var Question = _context.Tasks.Where(ixx=> ixx.QuestionId == id).ToList();
         
            var Task = _context.Tasks.Where(task => task.QuestionId == id).FirstOrDefault();
            var model = new AnswerViewModel { answers = allAnswer, TaskId = id, task = Task, TestId = Question.FirstOrDefault().TestId };
      
            return View(model);


        }
        [HttpPost]
        public IActionResult Edit(AnswerViewModel modal)
        {
            var answer = _context.Answers.Find(modal.AnswerId);

            var q = _context.Tasks.Where(i => i.QuestionId == answer.QuestionId).ToList().FirstOrDefault().QuestionType;
            var question = _context.Answers.Where(q => q.QuestionId == modal.TaskId).ToList();
            var isCoreectExist = question.Any(q => q.IsCorrect == true);

            if (q.Any()) { return View(modal); }

            if (isCoreectExist && q == "Radio")
            {
                TempData["Error"] = "Неможиво створити ще одну правильну відповідь, змініть тип запитання.";
                return View(modal);
            }

            answer.answerName = modal.AnswerName;
            answer.IsCorrect = modal.IsCorrect;

            _context.SaveChanges();

            return View(modal);
        }
        
        public IActionResult Create(AnswerViewModel AnswerViewModel)
        {

            var questionType = _context.Tasks.Where(q => q.QuestionId == AnswerViewModel.TaskId).FirstOrDefault().QuestionType;
            var question = _context.Answers.Where(q => q.QuestionId == AnswerViewModel.TaskId).ToList();
            var isCoreectExist = question.Any(q => q.IsCorrect == true);

            var answer = new Answer
            {
                answerName = AnswerViewModel.AnswerName,
                QuestionId = AnswerViewModel.TaskId,
                IsCorrect = AnswerViewModel.IsCorrect

            };

            if ((questionType == "Radio" && !isCoreectExist) || (questionType == "Radio" && isCoreectExist && !AnswerViewModel.IsCorrect) || ( questionType== "CheckBox"))
            {
                _context.Answers.Add(answer);
                _context.SaveChanges();
            }

            var allAnswer = _context.Answers.OrderByDescending(a => a.Id).Where(x => x.QuestionId == AnswerViewModel.TaskId).ToList();
            var model = new AnswerViewModel { answers = allAnswer };

            if (!questionType.Any()) { return View(model); }

            if (isCoreectExist && AnswerViewModel.IsCorrect && questionType == "Radio")
            {
                TempData["Error"] = "Неможиво створити ще одну правильну відповідь, змініть тип запитання.";
                return View(model);
            }

            return RedirectToAction("Details", new { id = answer.QuestionId });
        }
        public IActionResult Delete(int id)
        {
            var Answer = _context.Answers.Find(id);
            _context.Answers.Remove(Answer);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = Answer.QuestionId});
        }


    }
}
