using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdaController : ControllerBase
    {
        private readonly IOdaService _odaService;
        private readonly string _uploadDir;
        private readonly string _baseUrl;

        public OdaController(IOdaService odaService, IConfiguration config)
        {
            _odaService = odaService;

            // "wwwroot/uploads" dizinini hazırla
            _uploadDir = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads");
            Directory.CreateDirectory(_uploadDir);

            // appsettings.json içindeki BaseUrl (sonundaki slash'i kırpıyor)
            _baseUrl = config["AppSettings:BaseUrl"]?.TrimEnd('/')
                       ?? throw new ArgumentNullException("AppSettings:BaseUrl");
        }

        // GET: api/Oda
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var odalar = await _odaService.GetAllAsync();
                return Ok(odalar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 OdaController hatası: " + ex.Message);
                return StatusCode(500, "Sunucu hatası: " + ex.Message);
            }
        }

        // GET: api/Oda/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var oda = await _odaService.GetByIdAsync(id);
            if (oda == null) return NotFound();
            return Ok(oda);
        }

        // POST: api/Oda
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OdaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1) Kapak görseli Base64'iyse dosyaya kaydet
            if (!string.IsNullOrWhiteSpace(dto.GorselBase64))
            {
                var comma = dto.GorselBase64.IndexOf(',') + 1;
                var b64 = comma > 0
                             ? dto.GorselBase64.Substring(comma)
                             : dto.GorselBase64;
                var bytes = Convert.FromBase64String(b64);
                var fn = $"{Guid.NewGuid()}.jpg";
                var path = Path.Combine(_uploadDir, fn);
                await System.IO.File.WriteAllBytesAsync(path, bytes);

                // **Tam** URL atıyoruz
                dto.GorselUrl = $"{_baseUrl}/uploads/{fn}";
            }

            // 2) Foto galeri Base64 listesi varsa kaydet
            if (dto.FotografGaleriBase64?.Any() == true)
            {
                foreach (var galBase64 in dto.FotografGaleriBase64)
                {
                    var comma = galBase64.IndexOf(',') + 1;
                    var b64 = comma > 0
                                 ? galBase64.Substring(comma)
                                 : galBase64;
                    var bytes = Convert.FromBase64String(b64);
                    var fn = $"{Guid.NewGuid()}.jpg";
                    var path = Path.Combine(_uploadDir, fn);
                    await System.IO.File.WriteAllBytesAsync(path, bytes);

                    dto.FotografGaleriListesi.Add($"{_baseUrl}/uploads/{fn}");
                }
            }

            var created = await _odaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.OdaId }, created);
        }

        // PUT: api/Oda/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OdaDTO dto)
        {
            if (id != dto.OdaId)
                return BadRequest("ID uyuşmuyor.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Aynı Base64 → dosyaya kaydet mantığı
            if (!string.IsNullOrWhiteSpace(dto.GorselBase64))
            {
                var comma = dto.GorselBase64.IndexOf(',') + 1;
                var b64 = comma > 0
                             ? dto.GorselBase64.Substring(comma)
                             : dto.GorselBase64;
                var bytes = Convert.FromBase64String(b64);
                var fn = $"{Guid.NewGuid()}.jpg";
                var path = Path.Combine(_uploadDir, fn);
                await System.IO.File.WriteAllBytesAsync(path, bytes);
                dto.GorselUrl = $"{_baseUrl}/uploads/{fn}";
            }

            if (dto.FotografGaleriBase64?.Any() == true)
            {
                foreach (var galBase64 in dto.FotografGaleriBase64)
                {
                    var comma = galBase64.IndexOf(',') + 1;
                    var b64 = comma > 0
                                 ? galBase64.Substring(comma)
                                 : galBase64;
                    var bytes = Convert.FromBase64String(b64);
                    var fn = $"{Guid.NewGuid()}.jpg";
                    var path = Path.Combine(_uploadDir, fn);
                    await System.IO.File.WriteAllBytesAsync(path, bytes);
                    dto.FotografGaleriListesi.Add($"{_baseUrl}/uploads/{fn}");
                }
            }

            var updated = await _odaService.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/Oda/5
        [HttpDelete("{id}")]
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

