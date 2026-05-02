using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Domain.DTOs
{
    public class LoginDto
    {
        public string UsernameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}