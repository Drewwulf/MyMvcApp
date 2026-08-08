using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMvcApp.Models;
namespace MyMvcApp.Data
{
    internal class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

     public DbSet<Test> Tests { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Question> Tasks { get; set; }

     public DbSet<StudyGroup> studyGroups { get; set; }

        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<HomeworkInfo> HomeworkInfo { get; set; }

        public DbSet<Students> Students { get; set; }

        public DbSet<StudentToGroup> StudentToGroups { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<Documents> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Students>()
                .HasOne(s => s.group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.StudyGroupId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Schedule>()
            .HasOne(s => s.StudyGroup)
            .WithMany(g => g.Schedule)
            .HasForeignKey(s => s.StudyGroupId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Place)
                .WithMany()
                .HasForeignKey(s => s.PlaceId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<StudentsToHomework> StudentsToHomeworks { get; set; }

        public DbSet<ResultTest> ResultsTests { get; set; }
    }
}
