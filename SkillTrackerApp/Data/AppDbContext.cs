using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillTrackerApp.Models;

namespace SkillTrackerApp.Database
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<LearningGoal> LearningGoals { get; set; }

        protected AppDbContext()
        {
        }
    }
}
    