namespace MyMvcApp.Models
{
    public class HomeworkInfo
    {
        public int HomeworkInfoId { get; set; }
        public string Text { get; set; }
        public string Path { get; set; }

        public Homework Homework { get; set; }
        public Documents Documents { get; set; }
    }
}
