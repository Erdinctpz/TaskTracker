using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public ICollection<TodoItem>? TodoItems { get; set; }
        public ICollection<AppUserGroup>? UserGroups { get; set; }
    }
}