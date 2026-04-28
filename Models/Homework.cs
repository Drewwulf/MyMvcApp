namespace MyMvcApp.Models
{
    public class Homework

    {
        public int HomeworkId { get; set; } 
        public string HomeworkName { get; set; }     
        public string HomeworkDescription { get; set; }
        public bool isdeleted { get; set; } = false;
        public DateTime StartTime { get; set; }      // час початку
        public DateTime SubmitTime { get; set; }  
    }
}
 