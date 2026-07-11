namespace MyMvcApp.Models.ViewModels
{
    public class AnswerViewModel
    {

        public List<Answer> answers;

        public Question task { get; set; }

        public static object TestId { get; internal set; }
        public string TestName { get; set; }
        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public int TaskId { get; set; }
        public string AnswerName { get;  set; }
    }
}
