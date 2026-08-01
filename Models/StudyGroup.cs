namespace MyMvcApp.Models
{
    public class StudyGroup
    {
        internal object group;

        public int StudyGroupId { get; set; } 
        public int DirectionId { get; set; } 
        public int PlaceId { get; set; }
        public bool isdeleted { get; set; } = false;
        public string GroupName { get; set; }     
        public string GroupDescription { get; set; }   
        public Direction Direction { get; set; }= null!;
        public Place Place { get; set; }

        public int TeachersId { get; set; }   // foreign key
        public Teachers Teachers { get; set; } // navigation property
        public List<Students> Students { get; set; } = new List<Students>();

        public List<StudentToGroup> StudentToGroups { get; set;} = new List<StudentToGroup>();

        public List<Schedule> Schedule { get; set; } = new List<Schedule>();
        

    }
}