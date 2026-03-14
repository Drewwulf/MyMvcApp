using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;
namespace MyMvcApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

     public DbSet<Test> Tests { get; set; }
     public DbSet<Question> Tasks { get; set; }

     public DbSet<StudyGroup> studyGroups { get; set; }

        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Students> Students { get; set; }
        
    }
}
