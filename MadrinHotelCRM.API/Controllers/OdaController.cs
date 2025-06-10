using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdaController : ControllerBase
    {
        private readonly IOdaService _odaService;

        public OdaController(IOdaService odaService)
        {
            _odaService = odaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var odalar = await _odaService.GetAllAsync();
            return Ok(odalar);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var oda = await _odaService.GetByIdAsync(id);
            if (oda == null) return NotFound();
            return Ok(oda);
        }

        [HttpPost]
        

        public async Task<IActionResult> Create([FromBody] OdaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _odaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.OdaId }, created);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> Update(int id, [FromBody] OdaDTO dto)
        {
            if (id != dto.OdaId) return BadRequest("ID uyuşmuyor.");

            var updated = await _odaService.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> Delete(int id)
        {
            var success = await _odaService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // GET: api/Oda/tip/3
        [HttpGet("tip/{odaTipiId}")]
        public async Task<IActionResult> GetByOdaTipi(int odaTipiId)
        {
            var odalar = await _odaService.FindAsync(o => o.OdaTipiId == odaTipiId);
            return Ok(odalar);
        }
    }
}
