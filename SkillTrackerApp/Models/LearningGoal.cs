namespace SkillTrackerApp.Models
{
    public class LearningGoal
    {
        public int Id { get; set; }
        public string GoalTitle { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}