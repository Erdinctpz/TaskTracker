namespace TaskTracker.Application.DTOs
{
    public class TaskDto
    {
        public string Title { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
    }
}