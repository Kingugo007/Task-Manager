using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.DTOs
{
    public class CreateTaskDTO
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string TaskDescription { get; set; }
        [Required]
        public int AllottedTimeInDays { get; set; }
        [Required]
        public int ElapsedTimeInDays { get; set; }
        public bool TaskStatus { get; set; }
    }
}
