using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Domain.Entities
{
    [Table("Groups")]
    public class TaskGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string InviteCode { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string OwnerId { get; set; } = string.Empty;

        public ICollection<AppUserGroup>? UserGroups { get; set; }
        public ICollection<TodoItem>? Tasks { get; set; }
    }
}