namespace MyMvcApp.Models
{
    public class TestViewModel
    {
        public int DirectionId { get; set; } 
        public int TestId { get; set; } 
        public string Name { get; set; }     
        public string Description { get; set; }   
        public string Difficualty { get; set; }  

         public List<Test> test = new List<Test>();
         public List<Direction> directions = new List<Direction>();
    }
}