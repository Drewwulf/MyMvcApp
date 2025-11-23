using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class TestDirectionController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Test/Index.cshtml
        }
    }
}