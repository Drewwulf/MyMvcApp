using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Schedule/Index.cshtml
        }
    }
}