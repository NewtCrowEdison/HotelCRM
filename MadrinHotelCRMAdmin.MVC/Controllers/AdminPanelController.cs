using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly HttpClient _api;

        public AdminPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

      
        [HttpGet]
        public IActionResult Index() => View(); // Index.cshtml



        // Personel Kayıt
        [HttpPost]
        public async Task<IActionResult> PersonelKayit(KullaniciOlusturDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest("Model hatası: " + string.Join(" | ", errors));
            }

            var response = await _api.PostAsJsonAsync("api/authorization/kayit", model);

            if (response.IsSuccessStatusCode)
                return Ok("Personel kaydı ve kullanıcı oluşturma başarılı.");

            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest("API Hatası: " + apiError);
        }

        // Personel Güncelle
        [HttpPost]
        public async Task<IActionResult> PersonelGuncelle(PersonelDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest("Model hatası: " + string.Join(" | ", errors));
            }

            var response = await _api.PutAsJsonAsync("api/personel", dto);

            if (response.IsSuccessStatusCode)
                return Ok("Güncelleme başarılı!");

            var hata = await response.Content.ReadAsStringAsync();
            return BadRequest("API Hatası: " + hata);
        }

        // Personel Sil
        [HttpPost]
        public async Task<IActionResult> PersonelSil(int id)
        {
            var response = await _api.DeleteAsync($"api/personel/{id}");

            if (response.IsSuccessStatusCode)
                return Ok("Personel silindi.");

            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest("Silme hatası: " + apiError);
        }
    }
}
