using SkillTrackerApp.Models.DTOs;

namespace SkillTrackerApp.Models
{
    public class GoalsBySkillViewModel
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public List<LearningGoalDto> Goals { get; set; } = new();

        // Calculated property
        public int CompletedPercentage
        {
            get
            {
                if (Goals == null || !Goals.Any()) return 0;
                return (int)Math.Round(Goals.Count(g => g.IsCompleted) * 100.0 / Goals.Count);
            }
        }
    }
}
