using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdemeController : ControllerBase
    {
        private readonly IOdemeService _odemeService;

        public OdemeController(IOdemeService odemeService)
        {
            _odemeService = odemeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var odemeler = await _odemeService.GetAllAsync();
            return Ok(odemeler);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var odeme = await _odemeService.GetByIdAsync(id);
            if (odeme == null)
                return NotFound();
            return Ok(odeme);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OdemeDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _odemeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.OdemeId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OdemeDTO dto)
        {
            if (id != dto.OdemeId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _odemeService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _odemeService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
