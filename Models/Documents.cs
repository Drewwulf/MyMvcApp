using MyMvcApp.Models;

public class Documents
{
    public int Id { get; set; }

    public string FileName { get; set; }

    public string FilePath { get; set; }

    
    public int? HomeworkId { get; set; }
    public Homework Homework { get; set; }

    public int? HomeworkInfoId { get; set; }
    public HomeworkInfo HomeworkInfo { get; set; }
}