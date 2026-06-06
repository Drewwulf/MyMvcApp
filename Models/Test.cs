namespace MyMvcApp.Models
{
    public class Test
    {
        public int TestId { get; set; }

        public int DirectionId { get; set; }

        public string TestName { get; set; }
        public bool isdeleted { get; set; } = false;
        public string TestDescription { get; set; }

        public Direction Direction { get; set; }

        public string TestDifficualty { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}