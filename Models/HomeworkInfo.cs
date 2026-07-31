using MyMvcApp.Models;

public class HomeworkInfo
{
    public int HomeworkInfoId { get; set; }

    public string? Text { get; set; }

    public int HomeworkId { get; set; }
    public Homework Homework { get; set; }

    public int StudentId { get; set; }
    public Students Student { get; set; }

    public int? DocumentId { get; set; }
    public Documents? Document { get; set; }
}