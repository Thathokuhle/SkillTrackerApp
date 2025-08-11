using System.ComponentModel.DataAnnotations;

namespace SkillTrackerApp.Models
{
    public class LearningGoal
    {
        public int Id { get; set; }
        public string GoalTitle { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime TargetDate { get; set; }
        public bool IsCompleted { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }

        public int ProgressPercentage { get; set; }
    }
}