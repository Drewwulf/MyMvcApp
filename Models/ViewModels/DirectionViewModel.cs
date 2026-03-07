namespace MyMvcApp.Models
{
    public class DirectionViewModel
    {
        public int DirectionId { get; set; }
        public string DirectionName { get; set; }     
        public string DirectionDescription { get; set; }   
        public List<Direction> directions = new List<Direction>();
    }
}
     