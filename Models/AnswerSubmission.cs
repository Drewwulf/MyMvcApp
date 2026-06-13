namespace MyMvcApp.Models.ViewModels
{
    public class AnswerSubmission
    {
        public int QuestionId { get; set; }

        public List<int> SelectedAnswerId { get; set; } = new List<int>();
    }
}