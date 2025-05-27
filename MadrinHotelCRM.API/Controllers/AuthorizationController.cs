// API/Controllers/AuthorizationController.cs
using ServiceAuth = MadrinHotelCRM.Services.Interfaces.IAuthorizationService;
using MadrinHotelCRM.DTO;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MadrinHotelCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ServiceAuth _authSvc;

        public AuthorizationController(ServiceAuth authSvc)
            => _authSvc = authSvc;

        // POST: /api/authorization/giris
        [HttpPost("giris")]
        [AllowAnonymous]
        public async Task<IActionResult> Giris([FromBody] GirisDTO dto)
        {
            var result = await _authSvc.LoginAsync(dto);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Email veya şifre yanlış." });

            return Ok(new { message = "Hoş geldin!" });
        }

        // POST: /api/authorization/kayit
        [HttpPost("kayit")]
        [AllowAnonymous]
        public async Task<IActionResult> Kayit([FromBody] KullaniciOlusturDTO dto)
        {
            var result = await _authSvc.CreateUserAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = $"{dto.Rol} rolünde yeni kullanıcı oluşturuldu!" });
        }

        // POST: /api/authorization/cikis
        [HttpPost("cikis")]
        [Authorize]
        public async Task<IActionResult> Cikis()
        {
            await _authSvc.LogoutAsync();
            return Ok(new { message = "Oturum kapatıldı." });
        }
    }
}
