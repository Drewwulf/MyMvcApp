using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class DirectionController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Direction/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
    }
}
