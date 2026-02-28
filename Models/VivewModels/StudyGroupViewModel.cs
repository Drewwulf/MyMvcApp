
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Xml;

namespace MyMvcApp.Models
{
    public class StudyGroupViewModel
    {
        public int GrId { get; set; } 
        public int DirId { get; set; } 
        public int PlId { get; set; } 
        public string GroupName { get; set; }     
        public string GroupDescription { get; set; }
         public List<StudyGroup> studyGroup = new List<StudyGroup>();
         public List<Direction> directions = new List<Direction>();
        public List<Place> place = new List<Place>();
        public List<IdentityUser> users = new List<IdentityUser>();
    }
}