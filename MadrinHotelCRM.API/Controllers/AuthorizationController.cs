// API/Controllers/AuthorizationController.cs
using ServiceAuth = MadrinHotelCRM.Services.Interfaces.IAuthorizationService;
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
        private readonly IPersonelService _personelSvc;

        public AuthorizationController(
            ServiceAuth authSvc,
            IPersonelService personelSvc)
        {
            _authSvc = authSvc;
            _personelSvc = personelSvc;
        }

        // POST: /api/authorization/giris
        [HttpPost("giris")]
        [AllowAnonymous]
        public async Task<IActionResult> Giris([FromBody] GirisDTO dto)
        {
            var result = await _authSvc.LoginAsync(dto);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Email veya şifre yanlış." });

            // Kullanıcıyı bul ve rolünü al
            var user = await _authSvc.GetUserByEmailAsync(dto.Email);
            var roles = await _authSvc.GetRolesAsync(user); // bunu interface'e ekleyeceğiz
            var userRole = roles.FirstOrDefault() ?? "Personel";

            return Ok(new
            {
                message = "Hoş geldin!",
                role = userRole,
                kullaniciId = user.Id 
            });
        }

        // POST: /api/authorization/kayit
        [HttpPost("kayit")]
        [AllowAnonymous]
        public async Task<IActionResult> Kayit([FromBody] KullaniciOlusturDTO dto)
        {
            var user = await _authSvc.CreateUserAsync(dto);

            if (user == null)
                return BadRequest(new { message = "Kullanıcı oluşturulamadı." });

            if (dto.Rol == "Personel")
            {
                var personelDto = new PersonelDTO
                {
                    Ad = dto.Ad,
                    Soyad = dto.Soyad,
                    Email = dto.Email,
                    Telefon = dto.Telefon,
                    YabanciUyrukluMu = dto.YabanciUyrukluMu,
                    PasaportNo = dto.PasaportNo,
                    TcKimlik = dto.TcKimlik,
                    KullaniciId = user.Id, // ✅ Artık boş değil!
                    PasswordHash = dto.Sifre
                };

                try
                {
                    await _personelSvc.CreateAsync(personelDto);
                }
                catch (Exception ex)
                {
                    var fullMessage = ex.InnerException?.Message ?? ex.Message;
                    Console.WriteLine("🔥 INNER EXCEPTION 🔥");
                    Console.WriteLine(ex.ToString()); // Detaylı istisna

                    return BadRequest(new { message = "Kullanıcı oluşturuldu ancak personel kaydı başarısız: " + fullMessage });
                }
            }

            return Ok(new { message = $"{dto.Rol} rolünde kullanıcı başarıyla oluşturuldu." });
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
