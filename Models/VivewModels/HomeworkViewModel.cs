namespace MyMvcApp.Models
{
    public class HomeworkViewModel
    {
        public int HomeworkId { get; set; }
        public string HomeworkName { get; set; }     
        public string HomeworkDescription { get; set; }   
        public List<Homework> homeworks = new List<Homework>();
    }
}
     