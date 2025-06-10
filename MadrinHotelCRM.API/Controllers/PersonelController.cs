
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadrinHotelCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PersonelController : ControllerBase
    {
        private readonly IPersonelService _personelSvc;
        private readonly UserManager<AppUser> _userManager;

        public PersonelController(
            IPersonelService personelSvc,
            UserManager<AppUser> userManager)
        {
            _personelSvc = personelSvc;
            _userManager = userManager;
        }

        // GET: /api/personel
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PersonelDTO>>> GetAll()
        {
            var list = await _personelSvc.GetAllAsync();
            return Ok(list);
        }

        // POST: /api/personel
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Model doğrulaması başarısız." });

            var created = await _personelSvc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.PersonelId }, created);
        }

        // PUT: /api/personel/{id}
        [HttpPut("{id}")]
        [AllowAnonymous] // Test amaçlı
        public async Task<IActionResult> Update(int id, [FromBody] PersonelDTO dto)
        {
            if (id != dto.PersonelId)
                return BadRequest(new { message = "ID uyuşmuyor." });

            if (string.IsNullOrWhiteSpace(dto.Password))
                dto.Password = null;

            var updated = await _personelSvc.UpdateAsync(dto);
            if (updated == null)
                return NotFound(new { message = $"ID {id} ile personel bulunamadı." });

            return Ok(updated);
        }

        // DELETE: /api/personel/{id}
        [HttpDelete("{id}")]
        [AllowAnonymous] // Test amaçlı
        public async Task<IActionResult> Delete(int id)
        {
            
            var personel = await _personelSvc.GetByIdAsync(id);
            if (personel == null)
                return NotFound(new { message = $"ID {id} ile personel bulunamadı." });

            //  AspNetUsers tablosundan kullanıcıyı sileriz
            var user = await _userManager.FindByIdAsync(personel.KullaniciId);
            if (user != null)
            {
                var identityResult = await _userManager.DeleteAsync(user);
                if (!identityResult.Succeeded)
                    return BadRequest(new
                    {
                        message = "Kullanıcı silinemedi.",
                        errors = identityResult.Errors
                    });
            }

            //  Personel tablosundan sileriz
            var silindi = await _personelSvc.DeleteAsync(id);
            if (!silindi)
                return BadRequest(new { message = "Personel silinemedi." });
            return NoContent();
        }
    }
}
