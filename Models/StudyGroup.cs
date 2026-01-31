namespace MyMvcApp.Models
{
    public class StudyGroup
    {
        public int StudyGroupId { get; set; } 
        public int DirectionId { get; set; } 
        public int PlaceId { get; set; } 
        public string GroupName { get; set; }     
        public string GroupDescription { get; set; }   
        public Direction Direction { get; set; }= null!;
        public ICollection<Place> Places { get; set; }
    }
}