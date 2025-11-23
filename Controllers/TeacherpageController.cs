using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class TeacherpageController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Teacherpage/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Teacherpage/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            return View(); // повертає Views/Destination/Create.cshtml
        }
    }
}