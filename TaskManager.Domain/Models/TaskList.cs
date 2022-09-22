namespace TaskManager.Domain.Models
{
    public class TaskList
    {
        public string TaskName { get; set; } 
        public string TaskDescription { get; set; } 
        public DateTime StartDate { get; set; } 
        public int AllottedTimeInDays { get; set; } 
        public int ElapsedTimeInDays { get; set; }
        public bool TaskStatus { get; set; }
        public string TaskId { get; set; }
    }
}
