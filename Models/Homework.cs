using System.Reflection.Metadata;

namespace MyMvcApp.Models
{
    public class Homework

    {
        public int HomeworkId { get; set; } 
        public string HomeworkName { get; set; }     
        public string HomeworkDescription { get; set; }
        public bool isdeleted { get; set; } = false;
        public string FilePath { get; set; }
        public DateTime StartTime { get; set; }      // час початку
        public DateTime SubmitTime { get; set; }  

        public List<StudentsToHomework> StudentsToHomework { get; set; }
        public Documents Document { get; set; }
        public List<HomeworkInfo> HomeworkInfo { get; set; }
    }
}
 