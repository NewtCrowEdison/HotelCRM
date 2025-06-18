using System;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezervasyonController : ControllerBase
    {
        private readonly IRezervasyonService _rezervasyonService;

        public RezervasyonController(IRezervasyonService rezervasyonService)
        {
            _rezervasyonService = rezervasyonService;
        }

        // GET: api/rezervasyon
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rezervasyonService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/rezervasyon/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rezervasyonService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/rezervasyon
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RezervasyonDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Gerekli otomatik alan atamaları
            dto.OlusturmaTarihi = DateTime.UtcNow;
            dto.Durum = RezervasyonDurum.Onaylandı;
            dto.IptalTarihi = null;
            dto.IptalNedeni = null;

            var created = await _rezervasyonService.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = created.RezervasyonId },
                created
            );
        }

        // PUT: api/rezervasyon/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RezervasyonDTO dto)
        {
            if (id != dto.RezervasyonId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _rezervasyonService.UpdateAsync(dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/rezervasyon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _rezervasyonService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        // PUT: api/rezervasyon/durum-guncelle/5?yeniDurum=Dolu
        [HttpPut("durum-guncelle/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] RezervasyonDurum yeniDurum)
        {
            var updated = await _rezervasyonService.UpdateStatusAsync(id, yeniDurum);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpPut("iptal-et/{id}")]
        public async Task<IActionResult> Cancel(int id, [FromQuery] string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return BadRequest("İptal nedeni gerekli.");

            var result = await _rezervasyonService.CancelReservationAsync(id, reason);
            if (!result) return NotFound();

            return Ok("Rezervasyon iptal edildi.");
        }

        // POST: api/rezervasyon/{rezervasyonId}/paket-ekle/{paketId}
        [HttpPost("{rezervasyonId}/paket-ekle/{paketId}")]
        public async Task<IActionResult> AddPackage(int rezervasyonId, int paketId)
        {
            var result = await _rezervasyonService.AddPackageAsync(rezervasyonId, paketId);
            if (!result)
                return BadRequest("Paket eklenemedi.");
            return Ok("Paket eklendi.");
        }

        // DELETE: api/rezervasyon/{rezervasyonId}/paket-kaldir/{paketId}
        [HttpDelete("{rezervasyonId}/paket-kaldir/{paketId}")]
        public async Task<IActionResult> RemovePackage(int rezervasyonId, int paketId)
        {
            var result = await _rezervasyonService.RemovePackageAsync(rezervasyonId, paketId);
            if (!result)
                return BadRequest("Paket kaldırılamadı.");
            return Ok("Paket kaldırıldı.");
        }

        // GET: api/rezervasyon/{rezervasyonId}/paketler
        [HttpGet("{rezervasyonId}/paketler")]
        public async Task<IActionResult> GetPackages(int rezervasyonId)
        {
            var result = await _rezervasyonService.GetPackagesAsync(rezervasyonId);
            return Ok(result);
        }
    }
}
