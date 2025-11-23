using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Task/Index.cshtml
        }
    }
}