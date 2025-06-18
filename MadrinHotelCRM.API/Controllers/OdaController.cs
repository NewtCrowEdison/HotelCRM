using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MadrinHotelCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdaController : ControllerBase
    {
        private readonly IOdaService _odaService;
        private readonly IRezervasyonService _rezervasyonService;
        private readonly string _uploadDir;
        private readonly string _baseUrl;

        public OdaController(IOdaService odaService, IRezervasyonService rezervasyonService, IConfiguration config)
        {
            _odaService = odaService;
            _rezervasyonService = rezervasyonService;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var odalar = await _odaService.GetAllAsync();

                foreach (var oda in odalar)
                {
                    if (!string.IsNullOrWhiteSpace(oda.GorselUrl) && !oda.GorselUrl.StartsWith("http"))
                        oda.GorselUrl = $"{_baseUrl}/uploads/{oda.GorselUrl}";

                    // Galeri
                    if (oda.FotografGaleriListesi != null)
                    {
                        for (int i = 0; i < oda.FotografGaleriListesi.Count; i++)
                        {
                            var galeriItem = oda.FotografGaleriListesi[i];
                            if (!string.IsNullOrWhiteSpace(galeriItem) && !galeriItem.StartsWith("http"))
                                oda.FotografGaleriListesi[i] = $"{_baseUrl}/uploads/{galeriItem}";
                        }
                    }
                }

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

            // Kapak
            if (!string.IsNullOrWhiteSpace(oda.GorselUrl) && !oda.GorselUrl.StartsWith("http"))
                oda.GorselUrl = $"{_baseUrl}/uploads/{oda.GorselUrl}";

            // Galeri
            if (oda.FotografGaleriListesi != null)
            {
                for (int i = 0; i < oda.FotografGaleriListesi.Count; i++)
                {
                    var galeriItem = oda.FotografGaleriListesi[i];
                    if (!string.IsNullOrWhiteSpace(galeriItem) && !galeriItem.StartsWith("http"))
                        oda.FotografGaleriListesi[i] = $"{_baseUrl}/uploads/{galeriItem}";
                }
            }

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

        [HttpGet("{odaId}/reservations")]
        public async Task<IActionResult> GetReservationsByOda(int odaId)
        {
           
            var rezList = await _rezervasyonService.GetByOdaIdAsync(odaId);
            return Ok(rezList);
        }

        /// <summary>
        /// Odanın durumunu (Boş, Dolu vs.) günceller.
        /// Kullanımı: PUT api/oda/durum-guncelle/5?yeniDurum=Dolu
        /// </summary>
        [HttpPut("durum-guncelle/{odaId}")]
        public async Task<IActionResult> UpdateRoomStatus(int odaId, [FromQuery] string yeniDurum)
        {
            var ok = await _odaService.UpdateRoomStatusAsync(odaId, yeniDurum);
            if (!ok)
                return NotFound($"Oda bulunamadı veya '{yeniDurum}' geçerli bir durum değil.");
            return Ok($"Oda #{odaId} durumu '{yeniDurum}' olarak güncellendi.");
        }

        /// <summary>
        /// Odaya yeni tarife atar veya mevcut tarife bilgisini günceller.
        /// Kullanımı: PUT api/oda/tarife-guncelle/5/3
        /// </summary>
        
        [HttpPut("tarife-guncelle/{odaId}/{tarifeId}")]
        public async Task<IActionResult> UpdateTariff(int odaId, int tarifeId)
        {
            var ok = await _odaService.UpdateTariffAsync(odaId, tarifeId);
            if (!ok)
                return NotFound($"Oda #{odaId} veya Tarife #{tarifeId} bulunamadı.");
            return Ok($"Oda #{odaId} için tarife #{tarifeId} başarıyla güncellendi.");
        }


        [HttpGet("bos")]
        public async Task<IActionResult> GetBosOdalar()
        {
            var tumRez = await _rezervasyonService.GetAllAsync();
            var doluIds = tumRez
                .Where(r => r.Durum != RezervasyonDurum.İptalEdildi)
                .Select(r => r.OdaId)
                .Distinct();

            var tumOdalar = await _odaService.GetAllAsync();
            var bosOdalar = tumOdalar
                .Where(o => !doluIds.Contains(o.OdaId))
                .ToList();

            return Ok(bosOdalar);
        }

    }
}

