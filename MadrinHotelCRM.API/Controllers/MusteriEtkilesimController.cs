using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusteriEtkilesimController : ControllerBase
    {
        private readonly IEtkilesimService _musteriEtkilesimService;

        public MusteriEtkilesimController(IEtkilesimService musteriEtkilesimService)
        {
            _musteriEtkilesimService = musteriEtkilesimService;
        }

        // GET: api/musterietkilesim
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _musteriEtkilesimService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/musterietkilesim/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _musteriEtkilesimService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/musterietkilesim
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MusteriEtkilesimDTO musteriEtkilesimDto)
        {
            if (musteriEtkilesimDto == null)
            {
                return BadRequest("Müşteri etkileşim verisi boş olamaz.");
            }

            var createdMusteriEtkilesim = await _musteriEtkilesimService.CreateAsync(musteriEtkilesimDto);
            return CreatedAtAction(nameof(GetById), new { id = createdMusteriEtkilesim.MusteriEtkilesimId }, createdMusteriEtkilesim);
        }

        // PUT: api/musterietkilesim/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MusteriEtkilesimDTO musteriEtkilesimDto)
        {
            if (musteriEtkilesimDto == null || id != musteriEtkilesimDto.MusteriEtkilesimId)
            {
                return BadRequest("Müşteri etkileşim verisi geçersiz.");
            }

            var updatedMusteriEtkilesim = await _musteriEtkilesimService.UpdateAsync(musteriEtkilesimDto);
            if (updatedMusteriEtkilesim == null)
            {
                return NotFound();
            }

            return Ok(updatedMusteriEtkilesim);
        }

        // DELETE: api/musterietkilesim/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _musteriEtkilesimService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        // Özel bir endpoint ekleyebilirsiniz, örneğin tüm etkileşimleri bir müşteri ID'sine göre getirmek için
        [HttpGet("byMusteri/{musteriId}")]
        public async Task<IActionResult> GetByMusteriId(int musteriId)
        {
            var result = await _musteriEtkilesimService.FindAsync(x => x.MusteriID == musteriId);
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        
    }
}
