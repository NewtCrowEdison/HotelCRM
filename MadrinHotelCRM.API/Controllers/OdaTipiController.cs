using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdaTipiController : Controller
    {
        private readonly IOdaTipiService _odaTipiService;

        public OdaTipiController(IOdaTipiService odaTipiService)
        {
            _odaTipiService = odaTipiService;
        }

        // GET: api/OdaTipi
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var odaTipleri = await _odaTipiService.GetAllAsync();
            return Ok(odaTipleri);
        }

        // GET: api/OdaTipi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var odaTipi = await _odaTipiService.GetByIdAsync(id);
            if (odaTipi == null)
                return NotFound();
            return Ok(odaTipi);
        }

        // POST: api/OdaTipi
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OdaTipiDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _odaTipiService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.OdaTipiId }, created);
        }

        // PUT: api/OdaTipi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OdaTipiDTO dto)
        {
            if (id != dto.OdaTipiId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _odaTipiService.UpdateAsync(dto);
            return Ok(updated);
        }

        // DELETE: api/OdaTipi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _odaTipiService.DeleteAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // GET: api/OdaTipi/kapasite/2
        [HttpGet("kapasite/{kapasite}")]
        public async Task<IActionResult> GetByKapasite(int kapasite)
        {
            var odaTipleri = await _odaTipiService.GetByIdAsync(kapasite);
            return Ok(odaTipleri);
        }
    }
}

