using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusteriController : ControllerBase
    {
        private readonly IMusteriService _musteriService;

        public MusteriController(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        // GET: api/Musteri
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var musteriler = await _musteriService.GetAllAsync();
            return Ok(musteriler);
        }

        // GET: api/Musteri/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var musteri = await _musteriService.GetByIdAsync(id);
            if (musteri == null)
                return NotFound();

            return Ok(musteri);
        }

        // POST: api/Musteri
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MusteriDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _musteriService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.MusteriId }, result);
        }

        // PUT: api/Musteri/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MusteriDTO dto)
        {
            if (id != dto.MusteriId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _musteriService.UpdateAsync(dto);
            return Ok(updated);
        }

        // DELETE: api/Musteri/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _musteriService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // POST: api/Musteri/5/AssignTag/3
        [HttpPost("{musteriId}/AssignTag/{etiketId}")]
        public async Task<IActionResult> AssignTag(int musteriId, int etiketId)
        {
            var success = await _musteriService.AssignTagAsync(musteriId, etiketId);
            return success ? Ok() : BadRequest();
        }

        // DELETE: api/Musteri/5/RemoveTag/3
        [HttpDelete("{musteriId}/RemoveTag/{etiketId}")]
        public async Task<IActionResult> RemoveTag(int musteriId, int etiketId)
        {
            var success = await _musteriService.RemoveTagAsync(musteriId, etiketId);
            return success ? Ok() : BadRequest();
        }

        // GET: api/Musteri/5/Interactions
        [HttpGet("{musteriId}/Interactions")]
        public async Task<IActionResult> GetInteractions(int musteriId)
        {
            var interactions = await _musteriService.GetInteractionsAsync(musteriId);
            return Ok(interactions);
        }

        // GET: api/Musteri/5/Feedbacks
        [HttpGet("{musteriId}/Feedbacks")]
        public async Task<IActionResult> GetFeedbacks(int musteriId)
        {
            var feedbacks = await _musteriService.GetFeedbacksAsync(musteriId);
            return Ok(feedbacks);
        }
    }
}
