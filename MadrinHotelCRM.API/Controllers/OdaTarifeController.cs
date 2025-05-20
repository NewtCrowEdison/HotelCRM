
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OdaTarifeController : ControllerBase
    {
        private readonly IOdaTarifeService _odaTarifeService;

        public OdaTarifeController(IOdaTarifeService odaTarifeService)
        {
            _odaTarifeService = odaTarifeService;
        }

        // GET: api/OdaTarife
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var odaTarifeleri = await _odaTarifeService.GetAllAsync();
            return Ok(odaTarifeleri);
        }

        // GET: api/OdaTarife/Details/5/3
        [HttpGet("Details/{odaId}/{tarifeId}")]
        public async Task<IActionResult> GetById(int odaId, int tarifeId)
        {
            var odaTarife = await _odaTarifeService.GetDetailsAsync(odaId, tarifeId);
            if (odaTarife == null)
                return NotFound();

            return Ok(odaTarife);
        }

        // POST: api/OdaTarife
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OdaTarifeDTO odaTarifeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _odaTarifeService.AddAsync(odaTarifeDTO);
            return CreatedAtAction(nameof(GetById), new { odaId = odaTarifeDTO.OdaId, tarifeId = odaTarifeDTO.TarifeId }, odaTarifeDTO);
        }

        // DELETE: api/OdaTarife/5/3
        [HttpDelete("{odaId}/{tarifeId}")]
        public async Task<IActionResult> Delete(int odaId, int tarifeId)
        {
            var success = await _odaTarifeService.DeleteAsync(odaId, tarifeId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // GET: api/OdaTarife/ByOdaId/5
        [HttpGet("ByOdaId/{odaId}")]
        public async Task<IActionResult> GetByOdaId(int odaId)
        {
            var odaTarifeleri = await _odaTarifeService.GetByOdaIdAsync(odaId);
            if (odaTarifeleri == null || !odaTarifeleri.Any())
                return NotFound();

            return Ok(odaTarifeleri);
        }

        // GET: api/OdaTarife/ByTarifeId/3
        [HttpGet("ByTarifeId/{tarifeId}")]
        public async Task<IActionResult> GetByTarifeId(int tarifeId)
        {
            var odaTarifeleri = await _odaTarifeService.GetByTarifeIdAsync(tarifeId);
            if (odaTarifeleri == null || !odaTarifeleri.Any())
                return NotFound();

            return Ok(odaTarifeleri);
        }

        // GET: api/OdaTarife/ByOdaAndTarife/5/3
        [HttpGet("ByOdaAndTarife/{odaId}/{tarifeId}")]
        public async Task<IActionResult> GetByOdaIdAndTarifeId(int odaId, int tarifeId)
        {
            var odaTarifeleri = await _odaTarifeService.GetDetailsAsync(odaId, tarifeId);
            if (odaTarifeleri == null)
                return NotFound();

            return Ok(odaTarifeleri);
        }
    }

}