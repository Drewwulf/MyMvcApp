namespace MyMvcApp.Models.ViewModels
{
    public class StudentPageViewModel
    {
        public List<StudyGroup> groups { get; set; }
        public List<StudentToGroup> allGroups { get; set; }
        public List<Direction> directions { get; set; }
        public string user { get; set; }
        public string email { get; set; }
    }
}
