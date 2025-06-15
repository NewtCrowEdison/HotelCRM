using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class AdminPanelController : Controller
    {
        private readonly HttpClient _api;

        public AdminPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        // ---------- Sayfa yüklenirken API Base adresini ViewBag'e ekliyoruz ----------
        [HttpGet]
        public IActionResult Index()
        {
            // Örneğin "https://localhost:7225"
            ViewBag.ApiBase = _api.BaseAddress.ToString().TrimEnd('/');
            return View();
        }

        // ---------- PERSONEL CRUD ----------
        [HttpPost]
        public async Task<IActionResult> PersonelKayit(KullaniciOlusturDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Model hatası", details = errors });
            }

            var response = await _api.PostAsJsonAsync("api/authorization/kayit", model);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Personel ve kullanıcı oluşturuldu." });

            return BadRequest(new { message = "API hatası", details = content });
        }

        [HttpPost]
        public async Task<IActionResult> PersonelGuncelle(PersonelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Any())
                    .SelectMany(kvp => kvp.Value.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Model hatası", details = errors });
            }

            // Şifre boş gelmişse null bırak
            if (string.IsNullOrWhiteSpace(dto.Password))
                dto.Password = null;

            var response = await _api.PutAsJsonAsync($"api/personel/{dto.PersonelId}", dto);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Personel güncellendi." });

            return BadRequest(new { message = "API hatası", details = content });
        }

        [HttpPost]
        public async Task<IActionResult> PersonelSil(int id)
        {
            var response = await _api.DeleteAsync($"api/personel/{id}");
            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Personel ve kullanıcı silindi." });

            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(new { message = "Silme hatası", details = error });
        }

        // ---------- ODA CRUD (JSON + [IgnoreAntiforgeryToken] ile) ----------
        [HttpPost]
        [IgnoreAntiforgeryToken]             // JSON post’larında antiforgery sorun çıkarmasın
        public async Task<IActionResult> OdaEkle([FromBody] OdaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Geçersiz form verisi.", details = ModelState });

            var response = await _api.PostAsJsonAsync("api/oda", dto);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda eklendi." });

            return BadRequest(new { message = "Ekleme hatası", details = content });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OdaGuncelle([FromBody] OdaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Geçersiz form verisi.", details = ModelState });

            var response = await _api.PutAsJsonAsync($"api/oda/{dto.OdaId}", dto);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda güncellendi." });

            return BadRequest(new { message = "Güncelleme hatası", details = content });
        }

        [HttpPost]
        public async Task<IActionResult> OdaSil(int id)
        {
            var response = await _api.DeleteAsync($"api/oda/{id}");
            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda silindi." });

            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(new { message = "Silme hatası", details = error });
        }

        // ---------- EK PAKET CRUD ----------
        [HttpPost]
        public async Task<IActionResult> EkPaketOlustur(EkPaketDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _api.PostAsJsonAsync("api/paket", dto);
            return response.IsSuccessStatusCode
                ? Ok()
                : BadRequest(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> EkPaketGuncelle(EkPaketDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _api.PutAsJsonAsync($"api/paket/{dto.EkPaketId}", dto);
            return response.IsSuccessStatusCode
                ? Ok()
                : BadRequest(await response.Content.ReadAsStringAsync());
        }

        [HttpPost]
        public async Task<IActionResult> EkPaketSil(int id)
        {
            var response = await _api.DeleteAsync($"api/paket/{id}");
            return response.IsSuccessStatusCode
                ? Ok()
                : BadRequest(await response.Content.ReadAsStringAsync());
        }

        // ---------- ODA TİPİ CRUD ----------
        [HttpPost]
        public async Task<IActionResult> OdaTipiOlustur(OdaTipiDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _api.PostAsJsonAsync("api/odaTipi", dto);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda tipi eklendi." });

            return BadRequest(new { message = "Ekleme hatası", details = content });
        }

        [HttpPost]
        public async Task<IActionResult> OdaTipiGuncelle(OdaTipiDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _api.PutAsJsonAsync($"api/odaTipi/{dto.OdaTipiId}", dto);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda tipi güncellendi." });

            return BadRequest(new { message = "Güncelleme hatası", details = content });
        }

        [HttpPost]
        public async Task<IActionResult> OdaTipiSil(int id)
        {
            var response = await _api.DeleteAsync($"api/odaTipi/{id}");
            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda tipi silindi." });

            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(new { message = "Silme hatası", details = error });
        }
    }
}
