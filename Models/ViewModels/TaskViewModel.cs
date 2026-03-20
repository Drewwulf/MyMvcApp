namespace MyMvcApp.Models
{
    public class TaskViewModel
    {
        public int DirectionId { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public string Ansver1Name { get; set; }
        public string Ansver2Name { get; set; }
        public string Ansver3Name { get; set; }
        public string Ansver4Name { get; set; }
        public string Description { get; set; }
        public string Difficualty { get; set; }

        public List<Question> Question = new List<Question>();

    }
}
