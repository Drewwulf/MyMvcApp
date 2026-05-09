

namespace MyMvcApp.Models
{
    public class StudentsToHomework
    {
        public int StudentsToHomeworkId { get; set; }
        public int HomeworkId { get; set; }

        public int UserId { get; set; }

        public bool IsEnded { get; set; }

        public Students Student { get; set; }

        public Homework Homework { get; set; }
    }
}
