using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherpageController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TeacherpageController(SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> TeacherGroup()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            var teacher = _context.Teachers.Where(t=>t.UserId==id).Include(t=>t.Groups).FirstOrDefault();
            var groups = teacher.Groups.ToList();
            return View(groups); 
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Teacherpage/Edit.cshtml
        }
        public IActionResult Details(int id)
        {
            var group = _context.studyGroups
     .Where(t => t.StudyGroupId == id)
     .Include(t => t.StudentToGroups)
         .ThenInclude(stg => stg.student)
     .FirstOrDefault();
            var modal = new MyMvcApp.Models.ViewModels.StudyGroupViewModel
            {
                Group = group,
                GroupName = group.GroupName,
                GroupDescription = group.GroupDescription
            };
            return View(modal); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            return View(); // повертає Views/Destination/Create.cshtml
        }
       
        public IActionResult TeacherShedule()
        {
            return View(); // повертає Views/Destination/Create.cshtml
        }
        public IActionResult TeacherTests()
        {
            return View(); // повертає Views/Destination/Create.cshtml
        }
        public async Task<IActionResult> TeacherCabinet()
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            var teacher = _context.Teachers.Where(t => t.UserId == id).Include(t => t.Groups).FirstOrDefault();
            
            return View(teacher);
        }
    }
}