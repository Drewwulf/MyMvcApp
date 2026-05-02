namespace MyMvcApp.Models
{
    public class Question
    {
       
        public int QuestionId {get; set;}
        public string QuestionName {get; set;}
        public string QuestionDescription { get; set;}

        
        public int TestId {get; set;}
        public Test Test {get; set;}
    }
}