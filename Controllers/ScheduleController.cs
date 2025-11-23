using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Schedule/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Schedule/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
    }
}