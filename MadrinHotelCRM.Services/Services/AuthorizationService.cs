using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthorizationService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(GirisDTO dto)
        {
            Console.WriteLine($"🟡 Giriş Denemesi - Email: {dto.Email}");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                Console.WriteLine("🔴 Kullanıcı bulunamadı.");
                return SignInResult.Failed;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            Console.WriteLine($"🟢 Şifre kontrol sonucu: {result.Succeeded}");

            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(KullaniciOlusturDTO dto)
        {
            var user = new AppUser
            {
                Email = dto.Email,
                UserName = dto.Email,        // Giriş için gerekli
                EmailConfirmed = true        // Şartlı kontrol varsa login engelini kaldırır
            };

            var res = await _userManager.CreateAsync(user, dto.Sifre);

            if (!res.Succeeded)
                return res;

            await _userManager.AddToRoleAsync(user, dto.Rol);
            return res;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task LogoutAsync()
        {
            Console.WriteLine("🔚 Kullanıcı çıkış yapıyor...");
            await _signInManager.SignOutAsync();
        }
    }
}
