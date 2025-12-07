using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Student/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Student/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
         public IActionResult MyGroup()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult MySchedule()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
    }
}