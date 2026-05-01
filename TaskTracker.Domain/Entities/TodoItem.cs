using System.ComponentModel.DataAnnotations.Schema;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Domain
{
    [Table("Tasks")]
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; }
    }
}