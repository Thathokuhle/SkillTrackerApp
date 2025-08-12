namespace SkillTrackerApp.Models
{
    public class DashboardViewModel
    {
        public List<LearningGoal> ActiveGoals { get; set; } = new List<LearningGoal>();
        public int CompletedCount { get; set; }
        public int InProgressCount { get; set; }
    }
}