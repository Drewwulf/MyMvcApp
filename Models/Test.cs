namespace MyMvcApp.Models
{
    public class Test
    {
        public int TestId { get; set; } 
        public int DirectionId { get; set; } 
        public string TestName { get; set; }     
        public string TestDescription { get; set; }   
        public Direction Direction { get; set; }
        public string TestDifficualty { get; set; }
           public ICollection<Question> Tasks { get; set; } 


    }
}
