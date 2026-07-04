using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using static System.Formats.Asn1.AsnWriter;

namespace MyMvcApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private Students student;

        public StudentController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        private async Task<int> GetStudentId()
        {
            var user = await _userManager.GetUserAsync(User);

            var studentId = _context.Students
                .First(s => s.UserId == user.Id)
                .Id;

            return studentId;
        }

        private async Task<List<StudyGroup>> GetStudentsGroup()
        {
            var studentId = await GetStudentId();
            var groups = _context.StudentToGroups
         .Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Teachers)
         .Include(g => g.studyGroup)
             .ThenInclude(sg => sg.Place)
         .Include(g => g.student)
         .Where(g => g.StudentId == (int)studentId)
         .ToList();
            var group = _context.StudentToGroups
        .Select(g => g.studyGroup)
        .Distinct()
        .ToList();

            return group;
        }

        private async Task<List<Direction>> GetStudentsDirection()
        {
            var studentId = await GetStudentId();
            var groups = _context.StudentToGroups
         .ToList();
            var directionsID1 = _context.StudentToGroups
        .Select(g => g.studyGroup).Select(d => d.DirectionId)
        .Distinct()
        .ToList();
            var firstId = directionsID1.FirstOrDefault();

            var directions = _context.Directions
    .Where(d => directionsID1.Contains(d.DirectionId))
    .ToList();

            return directions;
        }

        public IActionResult Index()
        {
            var studentslist = _context.Students.ToList();

            return View(studentslist); // повертає Views/Student/Index.cshtml
        }
        public IActionResult Edit()
        {
            return View(); // повертає Views/Student/Edit.cshtml
        }
        public IActionResult Details()
        {
            return View(); // повертає Views/Destination/Details.cshtml
        }
        public async Task<IActionResult> MyGroup()
        {
            var group = await GetStudentsGroup();
            var model = new StudentPageViewModel { groups = group };
            return View(model);
        }

        public async Task<IActionResult> Directions()
        {
            var directions = await GetStudentsDirection();

            return View(directions);
        }

        public IActionResult Tests(int id)
        {
            var allTests = _context.Tests.Where(ts => ts.DirectionId == id).ToList();
            return View(allTests);
        }

        public IActionResult TestPage(int id)
        {
            var tasks = _context.Tasks.Where(ts => ts.TestId == id).Include(t => t.Test).Include(ans => ans.Answers).ToList();


            return View(tasks);
        }

        [HttpGet]
        public IActionResult ShowScore(double score, int total)
        {
            double percentage = total > 0 ? (double)score / total * 100 : 0;

            var viewModel = new ScoreViewModel
            {
                Score = score,
                TotalQuestions = total,
                Percentage = Math.Round(percentage, 1) 
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckTest(List<AnswerSubmission> submissions)
        {
            decimal correctAnswersCount = 0;
            int totalQuestions = submissions.Count;

            var studentId = await GetStudentId();
            foreach (var submission in submissions)
            {
                var question = _context.Tasks
                                       .Include(q => q.Answers)
                                       .FirstOrDefault(q => q.QuestionId == submission.QuestionId);

                if (question == null) continue;
                if (question.QuestionType == "CheckBox") 
                {
                    var correctAnswersIds = question.Answers
                                                    .Where(a => a.IsCorrect)
                                                    .Select(a => a.Id)
                                                    .ToList();

                    var userSelectedIds = submission.SelectedAnswerId ?? new List<int>();


                    if (userSelectedIds.Any())
                    {
                        decimal matchedAnswerscoof = (decimal)1 / correctAnswersIds.Count;
                        var matchedAnswersCount = matchedAnswerscoof * userSelectedIds.Count(id => correctAnswersIds.Contains(id));
                        correctAnswersCount += matchedAnswersCount;
                    }
                }
                else 
                {
                    var firstSelectedId = submission.SelectedAnswerId?.FirstOrDefault();

                    var selectedAnswer = question.Answers
                                                 .FirstOrDefault(a => a.Id == firstSelectedId);

                    if (selectedAnswer != null && selectedAnswer.IsCorrect)
                    {
                        correctAnswersCount++;
                    }
                }
            }
            var gett = new ResultTest { Score = Math.Round(totalQuestions > 0 ? correctAnswersCount / totalQuestions * 100 : 0, 1), TestId = _context.Tasks.Include(q => q.Answers).FirstOrDefault(q => q.QuestionId == submissions.First().QuestionId).TestId,StudentId = studentId, DateTime = DateTime.Now };
            _context.ResultsTests.Add(gett);
            _context.SaveChanges();
            return RedirectToAction("ShowScore", new { score = correctAnswersCount, total = totalQuestions });
        }

    
        public async Task<IActionResult> HistoryTest()
        {
            var studentId = await GetStudentId();
            var scoree = _context.ResultsTests.Where(s => s.StudentId==studentId).Include(t=>t.Test).ToList();
      
            
            return View(scoree); 
         }


        public async Task<IActionResult> MySchedule() {
            var group = await GetStudentsGroup();
            var schedules = _context.Schedules.Where(g => g.StudyGroupId == group.First().StudyGroupId).Include(sg => sg.Place).ToList();
            var viewModel = new SheduleViewModel
            {
                Schedules = schedules

            };
            return View(viewModel); // повертає Views/Destination/Details.cshtml
        }


        public async Task<IActionResult> Homework()
        {
            var user = await _userManager.GetUserAsync(User);
            var userInfo = _context.Students.Where(i => i.UserId == user.Id).Include(hs => hs.StudentsToHomework).ThenInclude(h => h.Homework).ToList();

            return View(userInfo); // повертає Views/Student/Homework.cshtml
        }
    }
}