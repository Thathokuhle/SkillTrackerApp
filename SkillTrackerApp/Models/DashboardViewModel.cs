using SkillTrackerApp.Models.DTOs;

namespace SkillTrackerApp.Models
{
    public class DashboardViewModel
    {
        public int TotalSkills { get; set; }
        public int TotalGoals { get; set; }
        public int CompletedGoals { get; set; }
        public int CompletedCount { get; set; }
        public int InProgressCount { get; set; }
        public List<LearningGoal> ActiveGoals { get; set; } = new List<LearningGoal>();
        public List<RecentActivityDto> RecentActivities { get; set; } = new List<RecentActivityDto>();
    }
}