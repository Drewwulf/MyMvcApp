namespace MyMvcApp.Models
{
    public class StudentToGroup
    {
        public int StudentToGroupId { get; set; }

        public int StudentId { get; set; }

        public int StudyGroupId { get; set; }

        public Students student {  get; set; }

        public StudyGroup studyGroup { get; set; }


    }
}
