using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class HomeWorkController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Homework/Index.cshtml
        }
    }
}