namespace MyMvcApp.Models.ViewModels
{
    public class SheduleViewModel
    {
        public int scheduleId { get; set; }

        public string PlaceId { get; set; }

        public List<WeekDay> DaysOfWeek { get; set; }

        public DateTime Time { get; set; }

        public Place Place { get; set; }

        public List<Place> Places { get; set; }
    }
}