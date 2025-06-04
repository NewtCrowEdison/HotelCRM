using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadrinHotelCRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class PersonelController : ControllerBase
    {
        private readonly IPersonelService _personelSvc;

        public PersonelController(IPersonelService personelSvc)
        {
            _personelSvc = personelSvc;
        }

        //GET: /api/personel
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<PersonelDTO>>> GetAll()
        {
            var list = await _personelSvc.GetAllAsync();
            return Ok(list);
        }

        //POST: /api/personel
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model doğrulaması başarısız.");

            await _personelSvc.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), null);
        }

        //PUT: /api/personel
        [HttpPut]
        [AllowAnonymous] // bu kısım test amaçlıdır kalkacak token sonrasında !!!!!
        public async Task<IActionResult> Update([FromBody] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model doğrulaması başarısız.");

            try
            {
                var updated = await _personelSvc.UpdateAsync(dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Personel bulunamadı: {ex.Message}");
            }
        }

        //DELETE: /api/personel/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personelSvc.DeleteAsync(id);

            if (!result)
                return NotFound($"ID {id} ile personel bulunamadı.");

            return NoContent(); // 204
        }
    }
}
