using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class AutorizationController : Controller
    {
        public IActionResult Login()
        {
            return View(); // повертає Views/Autorization/Login.cshtml
        }
      
    }
}