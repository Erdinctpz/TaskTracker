using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Domain.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad boş bırakılamaz.")]
        public string LastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyadı tekrarı zorunludur.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}