using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    //[AutoValidateAntiforgeryToken]
    [Route("[controller]/[action]")]
    public class AdminPanelController : Controller
    {
        private readonly HttpClient _api;

        public AdminPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Index() => View();

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

            // Eğer formda şifre boş geldiyse, null geçir:
            if (string.IsNullOrWhiteSpace(dto.Password))
                dto.Password = null;

            var response = await _api.PutAsJsonAsync(
                $"api/personel/{dto.PersonelId}", dto);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Personel güncellendi." });

            return BadRequest(new { message = "API hatası", details = content });
        }

        //[HttpPost]
        //public async Task<IActionResult> PersonelSil(int id)
        //{
        //    var response = await _api.DeleteAsync($"api/personel/{id}");
        //    if (response.IsSuccessStatusCode)
        //        return Ok(new { message = "Personel silindi." });

        //    var error = await response.Content.ReadAsStringAsync();
        //    return BadRequest(new { message = "Silme hatası", details = error });
        //}

        [HttpPost]
        public async Task<IActionResult> PersonelSil(int id)
        {
            var response = await _api.DeleteAsync($"api/personel/{id}");
            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Personel ve kullanıcı silindi." });

            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(new { message = "Silme hatası", details = error });
        }

        [HttpPost]
        public async Task<IActionResult> OdaEkle(OdaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kvp => kvp.Value.Errors.Any())
                    .Select(kvp => new {
                        Field = kvp.Key,
                        Messages = kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    });

                return BadRequest(new
                {
                    message = "Geçersiz form verisi.",
                    details = errors
                });
            }

            var response = await _api.PostAsJsonAsync("api/oda", dto);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return Ok(new { message = "Oda eklendi." });

            return BadRequest(new { message = "Ekleme hatası", details = content });
        }

        [HttpPost]
        public async Task<IActionResult> OdaGuncelle(OdaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Geçersiz form verisi." });

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
    }
}