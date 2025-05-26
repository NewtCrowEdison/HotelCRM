using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifeController : Controller
    {
        private readonly ITarifeService _tarifeService;

        public TarifeController(ITarifeService tarifeService)
        {
            _tarifeService = tarifeService;
        } // DI ile ITarifeSeervice alıyoruz ve servis metotlarına erişebiliyoruz

        [HttpGet] // Tüm tarifeleri listelememizi sağlar 
        public async Task<IActionResult> GetAll()
        {
            var result = await _tarifeService.GetAllAsync();
            return Ok(result);
        }
            

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _tarifeService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TarifeDTO dto)
        {
            if (!ModelState.IsValid) // eksik veya yanlış veri varsa 400 bad request döner
            {
                return BadRequest(ModelState);
            }
            var created = await _tarifeService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                                   new { id = created.TarifeId },
                                   created);
        }

     
        [HttpPut("{id}")]
        // Güncelleme
        public async Task<IActionResult> Update(int id, [FromBody] TarifeDTO dto)
        {
            if (id != dto.TarifeId) return BadRequest("ID uyuşmuyor!");
            var updated = await _tarifeService.UpdateAsync(dto);
            return Ok(updated);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _tarifeService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        
        [HttpPut("{id}/uygula-indirim")]
        public async Task<IActionResult> ApplyDiscount(int id, [FromQuery] decimal discountRate)
        {
            var updated = await _tarifeService.ApplyDiscountAsync(id, discountRate);
            if (updated == null) return NotFound("Tarife bulunamadı.");
            return Ok(updated);
        }

        // İndirimli tarifeleri listeler.
        [HttpGet("indirimli-tarifeler")]
        public async Task<IActionResult> GetDiscountedTariffs()
        {
            // Servisten indirim oranı > 0 olan tarifeleri çekeriz
            var discounted = await _tarifeService.GetDiscountedTariffsAsync();
            return Ok(discounted);
        }

       
        // Belirli bir odaya ait tarifeleri getirir.
        [HttpGet("oda-tarifeleri/{odaId}")]
        public async Task<IActionResult> GetRoomTariffs(int odaId)
        {
            // Servisten odaId'ye göre tarifeleri alırız
            var odaTarife = await _tarifeService.GetRoomTariffsAsync(odaId);
            return Ok(odaTarife);
        }
    }
}
