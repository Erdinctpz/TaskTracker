using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Domain.Entities
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<TodoItem>? TodoItems { get; set; }
    }
}