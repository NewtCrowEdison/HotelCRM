using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaketController : ControllerBase
    {
        private readonly IPaketService _paketService;

        public PaketController(IPaketService paketService)
        {
            _paketService = paketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var paketler = await _paketService.GetAllAsync();
            return Ok(paketler);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paket = await _paketService.GetByIdAsync(id);
            if (paket == null)
                return NotFound();
            return Ok(paket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EkPaketDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _paketService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EkPaketId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EkPaketDTO dto)
        {
            if (id != dto.EkPaketId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _paketService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paketService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        

        
    }
}
