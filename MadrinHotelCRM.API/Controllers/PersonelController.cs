// API/Controllers/PersonelController.cs
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadrinHotelCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]    // opsiyonel: sadece Admin görebilir
    public class PersonelController : ControllerBase
    {
        private readonly IPersonelService _personelSvc;

        public PersonelController(IPersonelService personelSvc)
            => _personelSvc = personelSvc;

        // GET: /api/personel
        [HttpGet, AllowAnonymous]
        public async Task<ActionResult<List<PersonelDTO>>> GetAll()
            => Ok(await _personelSvc.GetAllAsync());

        // POST: /api/personel
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _personelSvc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), null);
        }

        // PUT: /api/personel
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _personelSvc.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: /api/personel/{id}
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _personelSvc.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
        //personel şifre değiştirme işemi için
        [HttpPut("sifre")]
        [Authorize] // Şifre işlemine sadece giriş yapan erişebilsin
        public async Task<IActionResult> SifreDegistir([FromBody] ChangePasswordDTO dto)
        {
            try
            {
                var result = await _personelSvc.ChangePasswordAsync(dto);
                return Ok("Şifre başarıyla değiştirildi.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
        }

    }
}
