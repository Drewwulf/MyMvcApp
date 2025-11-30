using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class TaskController : Controller
    {
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
         public IActionResult Create()
        {
            return View(); // повертає Views/Task/Create.cshtml
        }
    }
}