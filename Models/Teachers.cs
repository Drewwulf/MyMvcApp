
namespace MyMvcApp.Models
{
    public class Teachers
    {
       public int Id { get; set; }
       public string UserId { get; set; }
       public string UserName { get; set; }

       public List<StudyGroup> Groups { get; set; } = new List<StudyGroup>();

    }
}
