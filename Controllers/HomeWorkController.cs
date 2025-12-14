using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class HomeWorkController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Homework/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Homework/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
    }
}