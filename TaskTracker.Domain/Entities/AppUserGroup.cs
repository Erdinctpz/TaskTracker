namespace TaskTracker.Domain.Entities
{
    public class AppUserGroup
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;
        public int GroupId { get; set; }
        public TaskGroup Group { get; set; } = null!;
        public DateTime JoinedAt { get; set; }
    }
}