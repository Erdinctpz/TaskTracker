using Microsoft.AspNetCore.Identity;

namespace TaskTracker.Presentation.Localization
{
    public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string? userName) => new IdentityError { Code = nameof(InvalidUserName), Description = $"'{userName}' geçersiz bir kullanıcı adı." };
        public override IdentityError InvalidEmail(string? email) => new IdentityError { Code = nameof(InvalidEmail), Description = $"'{email}' geçersiz bir e-posta adresi." };
        public override IdentityError DuplicateUserName(string? userName) => new IdentityError { Code = nameof(DuplicateUserName), Description = $"'{userName}' kullanıcı adı zaten kullanımda." };
        public override IdentityError DuplicateEmail(string? email) => new IdentityError { Code = nameof(DuplicateEmail), Description = $"'{email}' e-posta adresi zaten kullanımda." };
        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = nameof(PasswordTooShort), Description = $"Şifre en az {length} karakter uzunluğunda olmalıdır." };
    }
}