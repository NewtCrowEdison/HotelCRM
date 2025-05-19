using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
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

      
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FaturaDTO dto)
        {
            if (id != dto.FaturaId) return BadRequest("ID uyuşmuyor!");
            var updated = await _faturaService.UpdateAsync(dto);
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
