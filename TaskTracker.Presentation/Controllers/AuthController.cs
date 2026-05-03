using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.Domain.DTOs;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IValidator<LoginDto> loginDtoValidator,
        IValidator<RegisterDto> registerDtoValidator,
        IMapper mapper
    ) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var validationResult = await registerDtoValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Kayıt olurken bir hata oluştu.";
                return Json(new
                {
                    success = false,
                    message = errorMessage
                });
            }

            var newUser = mapper.Map<AppUser>(registerDto);

            var result = await userManager.CreateAsync(newUser, registerDto.Password);

            if (result.Succeeded)
            {
                return Json(new
                {
                    success = true,
                    message = "Kayıt başarılı, yönlendiriliyorsunuz..."
                });
            }

            var errorMsg = result.Errors.FirstOrDefault()?.Description ?? "Kayıt olurken bir hata oluştu.";
            return Json(new
            {
                success = false,
                message = errorMsg
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var validationResult = await loginDtoValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                var errorMsg = validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Kayıt olurken bir hata oluştu.";
                return Json(new
                {
                    success = false,
                    message = errorMsg
                });
            }

            var user = await userManager.FindByEmailAsync(loginDto.UsernameOrEmail);

            if (user == null)
            {
                user = await userManager.FindByNameAsync(loginDto.UsernameOrEmail);
            }

            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Kullanıcı adı/Email veya şifre hatalı."
                });
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return Json(new
                {
                    success = true,
                    message = "Giriş yapılıyor..."
                });
            }

            return Json(new
            {
                success = false,
                message = "Kullanıcı adı/Email veya şifre hatalı."
            });
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}