// Services/Services/AuthorizationService.cs
using MadrinHotelCRM.DTO;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<AppUser> _userMgr;
        private readonly SignInManager<AppUser> _signInMgr;
        private readonly RoleManager<IdentityRole> _roleMgr;

        public AuthorizationService(
            UserManager<AppUser> userMgr,
            SignInManager<AppUser> signInMgr,
            RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _signInMgr = signInMgr;
            _roleMgr = roleMgr;
        }

        public async Task<SignInResult> LoginAsync(GirisDTO model)
        {
            // Email üzerinden kullanıcıyı bulma ;
            var user = await _userMgr.FindByEmailAsync(model.Email);
            if (user == null)
                return SignInResult.Failed;

            // Şifre kontrolü + cookie oluşturma işlemi;
            return await _signInMgr.PasswordSignInAsync(
                user, model.Sifre, model.BeniHatirla, lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInMgr.SignOutAsync();
        }

        public async Task<IdentityResult> CreateUserAsync(KullaniciOlusturDTO model)
        {
            // Yeni kullanıcı oluşturmak için kullanıcı nesnesi;
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            // yeni oluşturulan kullanıcıyı veritabanına ekleme işlemi
            var result = await _userMgr.CreateAsync(user, model.Sifre);
            if (!result.Succeeded)
                return result;

            //// Rol yoksa oluştur
            //if (!await _roleMgr.RoleExistsAsync(model.Rol))
            //    await _roleMgr.CreateAsync(new IdentityRole(model.Rol));

            // Kullanıcıya rol atama işlemi
            await _userMgr.AddToRoleAsync(user, model.Rol);
            return IdentityResult.Success;
        }
    }
}
