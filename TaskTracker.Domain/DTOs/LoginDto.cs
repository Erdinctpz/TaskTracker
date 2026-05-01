using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Domain.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Kullanıcı adı veya emailinizi girin.")]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string Password { get; set; } = string.Empty;
    }   
}