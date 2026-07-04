namespace MyMvcApp.Models
{
    public class ResultTest
    {
        public int Id { get; set; }

        public int TestId { get; set; }
        public int StudentId { get; set; }

        public decimal Score { get; set; }
        public Test Test { get; set; }

        public TestDifficualtyEnum TestDifficualty { get; set; }
        public DateTime DateTime { get; set; }
    }
}