using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class DestinationController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Destination/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Destination/Edit.cshtml
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