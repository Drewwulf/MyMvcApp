using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;

namespace MyMvcApp.Controllers
{
    public class AnswerController
    {

        private readonly ApplicationDbContext _context;
        private object model;

        public AnswerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Create(AnswerViewModel AnswerViewModel,int id)
        {



            return View();

           


        }

        private IActionResult View(object model)
        {
            throw new NotImplementedException();
        }

        private IActionResult RedirectToAction(string v)
        {
            throw new NotImplementedException();
        }
    }
}
