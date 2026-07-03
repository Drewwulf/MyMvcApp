namespace MyMvcApp.Models.ViewModels
{
    public class StudentDirectionViewModel
    {
        public int StudentLevel { get; set; }
        public int StudentPoints { get; set; }
        public required List<Direction> directions { get; set; }
    }
}
