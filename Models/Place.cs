namespace MyMvcApp.Models
{
    public class Place  
    {
        public int PlaceId { get; set; } 
        public string DestinationName { get; set; }     
        public string DestinationAddress { get; set; } 
        public List <StudyGroup> studyGroup { get; set; }
        public bool isdeleted { get; set; } = false;
    }
}
