// Services/Services/AuthorizationService.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;   // <-- bu arayüzü kullan

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
            // 1) Email’den kullanıcıyı bul
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return SignInResult.Failed;

            // 2) Şifreyi kontrol et (hash’li şifreyle karşılaştırır)
            var result = await _signInManager.CheckPasswordSignInAsync(
                user, dto.Password, lockoutOnFailure: false);

            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(KullaniciOlusturDTO dto)
        {
            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email
            };
            // 1) Kullanıcıyı ekle
            var res = await _userManager.CreateAsync(user, dto.Sifre);
            if (!res.Succeeded)
                return res;

            // 2) Rol ataması
            await _userManager.AddToRoleAsync(user, dto.Rol);
            return res;
        }

        public async Task LogoutAsync()
            => await _signInManager.SignOutAsync();
    }
}
