using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMvcApp.Data;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
        public class GroupsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public GroupsController(ApplicationDbContext context)
        {
            _context = context;
        } 
        public IActionResult Index()
        {
            return View(); // повертає Views/Group/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Group/Edit.cshtml
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