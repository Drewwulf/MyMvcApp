namespace MyMvcApp.Models
{
    public class Question
    {
        public int QuestionId {get; set;}
        public string QuestionName {get; set;}

        public string Ansver1Name {get; set;}
        public string Ansver2Name {get; set;}
        public string Ansver3Name {get; set;}
        public string Ansver4Name {get; set;}
        
        public int TestId {get; set;}
        public Test Test {get; set;}
    }
}