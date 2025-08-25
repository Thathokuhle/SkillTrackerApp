namespace SkillTrackerApp.Models.DTOs
{
    public class LearningGoalDto
    {
        public int Id { get; set; }
        public string GoalTitle { get; set; }
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }
        public int SkillId { get; set; }
        public string UserId { get; set; }
        public int ProgressPercentage { get; set; }
        public string SkillName { get; set; }
    }
}
