using System.Linq.Expressions;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeriBildirimController : ControllerBase
    {
        private readonly IGeriBildirimService _geriBildirimService;

        public GeriBildirimController(IGeriBildirimService geriBildirimService)
        {
            _geriBildirimService = geriBildirimService;
        }

        // GET: api/geribildirim
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _geriBildirimService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/geribildirim/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _geriBildirimService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/geribildirim
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GeriBildirimDTO geriBildirimDto)
        {
            if (geriBildirimDto == null)
            {
                return BadRequest("Geri bildirim verisi boş olamaz.");
            }

            var createdGeriBildirim = await _geriBildirimService.CreateAsync(geriBildirimDto);
            return CreatedAtAction(nameof(GetById), new { id = createdGeriBildirim.GeriBildirimId }, createdGeriBildirim);
        }

        // PUT: api/geribildirim/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GeriBildirimDTO geriBildirimDto)
        {
            if (geriBildirimDto == null || id != geriBildirimDto.GeriBildirimId)
            {
                return BadRequest("Geri bildirim verisi geçersiz.");
            }

            var updatedGeriBildirim = await _geriBildirimService.UpdateAsync(geriBildirimDto);
            if (updatedGeriBildirim == null)
            {
                return NotFound();
            }

            return Ok(updatedGeriBildirim);
        }

        // DELETE: api/geribildirim/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _geriBildirimService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // Ekstra: Geri bildirimleri filtreleme
        [HttpGet("filter")]
        public async Task<IActionResult> Find(Expression<Func<GeriBildirim, bool>> predicate)
        {
            var result = await _geriBildirimService.FindAsync(predicate);
            return Ok(result);
        }
        
    }
}