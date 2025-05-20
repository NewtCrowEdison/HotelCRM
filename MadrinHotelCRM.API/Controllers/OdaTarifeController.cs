
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET: api/OdaTarife/details/odaId/tarifeId
        [HttpGet("details/{odaId}/{tarifeId}")]
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

        // DELETE: api/OdaTarife/odaId/tarifeId
        [HttpDelete("{odaId}/{tarifeId}")]
        public async Task<IActionResult> Delete(int odaId, int tarifeId)
        {
            var success = await _odaTarifeService.DeleteAsync(odaId, tarifeId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // GET: api/OdaTarife/by-oda/5
        [HttpGet("by-oda/{odaId}")]
        public async Task<IActionResult> GetByOdaId(int odaId)
        {
            var odaTarifeleri = await _odaTarifeService.GetByOdaIdAsync(odaId);
            if (odaTarifeleri == null || !odaTarifeleri.Any())
                return NotFound();

            return Ok(odaTarifeleri);
        }

        // GET: api/OdaTarife/by-tarife/5
        [HttpGet("by-tarife/{tarifeId}")]
        public async Task<IActionResult> GetByTarifeId(int tarifeId)
        {
            var odaTarifeleri = await _odaTarifeService.GetByTarifeIdAsync(tarifeId);
            if (odaTarifeleri == null || !odaTarifeleri.Any())
                return NotFound();

            return Ok(odaTarifeleri);
        }
    }
}