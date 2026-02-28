using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMvcApp.Data;
using MyMvcApp.Models;
using System.Text.RegularExpressions;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

       

        public GroupsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View(); // повертає Views/Group/Index.cshtml
        }
        public IActionResult Edit(int id)
        {
            var GroupPlace =_context.studyGroups.Find(id);
            var modal = new StudyGroupViewModel
            {
                GrId = id,
                GroupName= GroupPlace.GroupName,
                GroupDescription=GroupPlace.GroupDescription,
                directions = _context.Directions.ToList(),
                place = _context.Places.ToList()
            };
            return View(modal); // повертає Views/Direction/Edit.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Edit(StudyGroupViewModel modal)
        {
        
             var group = _context.studyGroups.Find(modal.GrId);


    group.GroupName = modal.GroupName;
    group.GroupDescription = modal.GroupDescription;
    group.PlaceId = modal.PlId;
    group.DirectionId=modal.DirId;
     var allGroups = _context.studyGroups.ToList();
            var allDirections = _context.Directions.ToList();
            var model = new StudyGroupViewModel{ studyGroup = allGroups,directions = allDirections,place = _context.Places.ToList()};

    _context.SaveChanges();

            return View(modal); // повертає Views/Direction/Edit.cshtml
        }
        public IActionResult Details(int id){

        var Group = _context.studyGroups.Find(id); 
          var modal = new StudyGroupViewModel
          {
            GroupName =Group.GroupName,
            GroupDescription=Group.GroupDescription,
            studyGroup = _context.studyGroups.Include(g => g.Direction).Include(g => g.Places).ToList(),
            GrId = id,
            PlId = Group.PlaceId
          };

            return View(modal); // повертає Views/Destination/Details.cshtml
        }
        public IActionResult Create()
        {
            var allGroups = _context.studyGroups.ToList();
            var allDirections = _context.Directions.ToList();
            var model = new StudyGroupViewModel{ studyGroup = allGroups,directions = allDirections,place = _context.Places.ToList()};
            var users =  _userManager.Users.OrderBy(u => u.UserName).ToListAsync();
            return View(model); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudyGroupViewModel StudyGroupViewModel)
        {
            var groups = _context.studyGroups.Find(
                StudyGroupViewModel.GrId);

            var groups1 = new StudyGroup
            {
                GroupName = StudyGroupViewModel.GroupName,
                GroupDescription = StudyGroupViewModel.GroupDescription,
                DirectionId = StudyGroupViewModel.DirId,
                PlaceId = StudyGroupViewModel.PlId,
            };
            _context.studyGroups.Add(groups1);
            _context.SaveChanges();

            return RedirectToAction("Create"); 
        }
    
    }
    }