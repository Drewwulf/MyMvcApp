namespace MyMvcApp.Models
{
    public class Question
    {
       
        public int QuestionId {get; set;}
        public string QuestionName {get; set;}
        public string QuestionDescription { get; set;}
        public string QuestionType { get; set; }

        
        public int TestId {get; set;}
        public Test Test {get; set;}

        public List<Answer> Answers {get; set;}

        public bool isdeleted { get; set; } = false;

    }
}