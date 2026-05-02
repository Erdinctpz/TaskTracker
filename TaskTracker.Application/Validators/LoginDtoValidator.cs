using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TaskTracker.Domain.DTOs;

namespace TaskTracker.Application.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty().WithMessage("Kullanıcı adı veya emailinizi girin.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.");
        }
    }
}