using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace MyMvcApp.Models
{
    public class Students
    {
        public int Id { get; set; }

        public string Username {  get; set; }
        
        public string UserId {  get; set; }

        public int? StudyGroupId { get; set; } = 0;

        public int StudentPoints { get; set; } = 0;

        public StudyGroup group { get; set; }

        public List<StudentToGroup>  studentToGroups { get; set; }

        public List<StudentsToHomework> StudentsToHomework { get; set; }

    }
}
