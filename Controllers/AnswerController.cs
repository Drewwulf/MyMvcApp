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
                IsCorrect = answer.IsCorrect
            };

            return View(modal);
        }
        public IActionResult Details(int id)
        {

            var allAnswer = _context.Answers.Where(x => x.QuestionId == id).ToList();
            var model = new AnswerViewModel { answers = allAnswer,TaskId= id };
            return View(model);


        }
        [HttpPost]
        public IActionResult Edit(AnswerViewModel modal)
        {
            var answer = _context.Answers.Find(modal.AnswerId);

            answer.answerName = modal.AnswerName;
            answer.IsCorrect = modal.IsCorrect;

            _context.SaveChanges();

            return View(modal);
        }

        public IActionResult Create(AnswerViewModel AnswerViewModel)
        {
            var answer = new Answer
            {
                answerName = AnswerViewModel.AnswerName,
                QuestionId = AnswerViewModel.TaskId,
                IsCorrect = AnswerViewModel.IsCorrect

            };
            _context.Answers.Add(answer);
            _context.SaveChanges();



            var allAnswer = _context.Answers.OrderByDescending(a => a.Id).Where(x => x.QuestionId == AnswerViewModel.TaskId).ToList();
            var model = new AnswerViewModel { answers = allAnswer };
            return View(model);
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
