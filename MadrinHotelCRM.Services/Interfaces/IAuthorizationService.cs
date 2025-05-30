using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace MadrinHotelCRM.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<SignInResult> LoginAsync(GirisDTO model);
        Task LogoutAsync();
        Task<IdentityResult> CreateUserAsync(KullaniciOlusturDTO model);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(AppUser user);
    }
}
