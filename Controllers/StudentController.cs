using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Student/Index.cshtml
        }
    }
}