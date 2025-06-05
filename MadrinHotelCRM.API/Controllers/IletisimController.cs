using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class IletisimController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] IletisimFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // veritabanına kayıt, e-posta gönderme loglama yapılabilir.
            return Ok("Mesaj başarıyla alındı.");
        }
    }
}
