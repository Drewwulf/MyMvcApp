namespace MyMvcApp.Models
{
    public class Answer
    {

        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string answerName { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }  
       
       
    } 
}
