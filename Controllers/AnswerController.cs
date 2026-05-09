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
        public IActionResult Create(AnswerViewModel AnswerViewModel,int id)
        {

            var allAnswer = _context.Answers.Where(x => x.QuestionId == id).ToList();
            var model = new AnswerViewModel { answers = allAnswer,TaskId= id };
            return View(model);


        }
        [HttpPost]
        
        public IActionResult Create(AnswerViewModel AnswerViewModel)
        {
            var answer = new Answer
            {
                answerName = AnswerViewModel.answername,
                QuestionId = AnswerViewModel.TaskId,
                IsCorrect = AnswerViewModel.IsCorrect

            };
            _context.Answers.Add(answer);
            _context.SaveChanges();



            var allAnswer = _context.Answers.Where(x => x.QuestionId == AnswerViewModel.TaskId).ToList();
            var model = new AnswerViewModel { answers = allAnswer };
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var Answer = _context.Answers.Find(id);
            _context.Answers.Remove(Answer);
            _context.SaveChanges();
            return RedirectToAction("Create", new { id = Answer.QuestionId});
        }


    }
}
