namespace MyMvcApp.Models
{
    public class Schedule
    {
        public int Id { get; set; }

        public int PlaceId { get; set; }

        public int StudyGroupId { get; set; }

        public DateOnly  dayDestination{ get; set; }

        public WeekDay DayOfWeek { get; set; }

        public DateTime startTime { get; set; }

        public DateTime endTime { get; set; }

        public Place Place { get; set; }

        public StudyGroup StudyGroup { get; set; }
    }
}
