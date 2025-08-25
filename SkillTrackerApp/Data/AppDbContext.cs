using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillTrackerApp.Models;
using SkillTrackerApp.Models.DTOs;

namespace SkillTrackerApp.Database
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<LearningGoal> LearningGoals { get; set; }

        public DbSet<LearningGoalDto> LearningGoalDtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // DTO is query-only, no table
            modelBuilder.Entity<LearningGoalDto>().HasNoKey().ToView(null);
        }
        protected AppDbContext()
        {
        }
    }
}
    