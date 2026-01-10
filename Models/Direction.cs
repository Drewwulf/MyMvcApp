namespace MyMvcApp.Models
{
    public class Direction
    {
        public int DirectionId { get; set; } 
        public string DirectionName { get; set; }     
        public string DirectionDescription { get; set; }  
           public ICollection<Test> Tests { get; set; } 
    }
}
