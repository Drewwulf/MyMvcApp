namespace MyMvcApp.Models.ViewModels
{
    public class SheduleViewModel
    {
        public int scheduleId { get; set; }

        public int StudyGroupId { get; set; }

        public int PlaceId { get; set; }

        public List<WeekDay> DaysOfWeek { get; set; }

        public WeekDay DayOfWeek { get; set; }

        public DateTime Time { get; set; }

        public DateTime EndTime { get; set; }

        public Place Place { get; set; }

        public List<Place> Places { get; set; }
    }
}