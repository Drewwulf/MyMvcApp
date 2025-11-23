using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class TeacherpageController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Teacherpage/Index.cshtml
        }
    }
}