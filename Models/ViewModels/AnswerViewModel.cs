namespace MyMvcApp.Models.ViewModels
{
    public class AnswerViewModel
    {

        public List<Answer> answers;

        public static object TestId { get; internal set; }
        public bool IsCorrect { get; set; }
        public int TaskId { get; set; }
        public string answername { get;  set; }
    }
}
