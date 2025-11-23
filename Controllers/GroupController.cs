using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Group/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Group/Edit.cshtml
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