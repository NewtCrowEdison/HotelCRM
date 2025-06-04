using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EtiketController : ControllerBase
    {
        private readonly IEtiketService _etiketService;

        public EtiketController(IEtiketService etiketService)
        {
            _etiketService = etiketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var etiketler = await _etiketService.GetAllAsync();
            return Ok(etiketler);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var etiket = await _etiketService.GetByIdAsync(id);
            if (etiket == null)
                return NotFound();
            return Ok(etiket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EtiketDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _etiketService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EtiketId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EtiketDTO dto)
        {
            if (id != dto.EtiketId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _etiketService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _etiketService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }

 }