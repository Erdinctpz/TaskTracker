using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentValidation;
using TaskTracker.Domain.DTOs;

namespace TaskTracker.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş bırakılamaz.")
                .MaximumLength(20).WithMessage("Ad 20 karakterden fazla olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş bırakılamaz.")
                .MaximumLength(20).WithMessage("Soyad 20 karakterden fazla olamaz.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MaximumLength(20).WithMessage("Kullanıcı adı 20 karakterden fazla olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.")
                .MaximumLength(8).WithMessage("Şifre en fazla 8 karakter olmalı.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre tekrar alanı boş bırakılamaz.")
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
        }
    }
}