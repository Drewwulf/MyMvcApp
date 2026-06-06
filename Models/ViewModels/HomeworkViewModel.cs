namespace MyMvcApp.Models
{
    public class HomeworkViewModel
    {
        public int HomeworkId { get; set; }
        public int StudentId { get;set; }
        public string HomeworkName { get; set; }     
        public string HomeworkDescription { get; set; }   

        public DateTime StartTime { get; set; }      // час початку
        public DateTime SubmitTime { get; set; }
        public List<Homework> homeworks = new List<Homework>();

        public List<Students> students = new List<Students>();

        public List<StudentsToHomework> studentsToHomeworks = new List<StudentsToHomework>();
    }
}
     