using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class DirectionController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Direction/Index.cshtml
        }
    }
}
