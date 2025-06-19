using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : Controller
    {
        private readonly IFaturaService _faturaService;

        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await _faturaService.GetAllAsync();
            return Ok(result);
        }
       

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _faturaService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FaturaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _faturaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                                   new { id = created.FaturaId },
                                   created);
        }

        // POST api/fatura/olustur/{rezervasyonId}
        [HttpPost("olustur/{rezervasyonId}")]
        public async Task<IActionResult> CreateFromRezervasyon(int rezervasyonId)
        {
            var dto = await _faturaService.CreateFromRezervasyonAsync(rezervasyonId);
            if (dto == null)
                return NotFound($"Rezervasyon {rezervasyonId} bulunamadı.");

            return Ok(dto);
        }

        [HttpPost("durum-guncelle/{id}")]
        public async Task<IActionResult> UpdateStatusViaPost(
               int id,
               [FromQuery] FaturaDurum yeniDurum)
        {
            var updated = await _faturaService.UpdateStatusAsync(id, yeniDurum);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _faturaService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
