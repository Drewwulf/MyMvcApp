namespace MyMvcApp.Models
{
    public class TaskViewModel
    {
        public int DirectionId { get; set; }
        public int TestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Difficualty { get; set; }

        public List<Question> Question = new List<Question>();

    }
}
