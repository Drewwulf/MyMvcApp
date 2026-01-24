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

        public DbSet<Place> Places { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
