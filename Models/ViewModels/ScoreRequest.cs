using MyMvcApp.Models;

public class ScoreRequest
{
    public decimal Score { get; set; }
    public int Total { get; set; }
    public TestDifficualtyEnum TestDifficualty { get; set; }
}