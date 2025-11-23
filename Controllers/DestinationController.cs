using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class DestinationController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Destination/Index.cshtml
        }
    }
}