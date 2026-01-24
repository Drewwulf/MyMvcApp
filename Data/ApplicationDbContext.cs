using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models; // підключаємо моделі

namespace MyMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

 public DbSet<Place> Places { get; set; }
  public DbSet<Direction> Directions{ get; set; }
    public DbSet<Homework> Homeworks { get; set; }

     public DbSet<Test> Tests { get; set; }
     public DbSet<Question> Tasks { get; set; }

    }
}
