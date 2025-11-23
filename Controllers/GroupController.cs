using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View(); // повертає Views/Group/Index.cshtml
        }
    }
}