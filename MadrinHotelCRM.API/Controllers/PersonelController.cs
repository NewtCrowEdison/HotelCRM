using System.Linq.Expressions;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    public class PersonelController : Controller
    {
        private readonly IPersonelService _personelService;
        public PersonelController(IPersonelService personelService)
        {
            _personelService = personelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var personel = await _personelService.GetByIdAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            return View(personel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personelList = await _personelService.GetAllAsync();
            return View(personelList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personelService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.PersonelId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonelDTO dto)
        {
            if (id != dto.PersonelId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _personelService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _personelService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Find(Expression<Func<Personel, bool>> predicate)
        {
            var personelList = await _personelService.FindAsync(predicate);
            return View(personelList);
        }

    }
}
