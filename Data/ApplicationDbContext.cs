using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models; // підключаємо моделі

namespace MyMvcApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

 public DbSet<Place> Places { get; set; }
    }
}
