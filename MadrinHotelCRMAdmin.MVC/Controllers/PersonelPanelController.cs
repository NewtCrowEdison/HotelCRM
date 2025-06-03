using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class PersonelPanelController : Controller
    {
        private readonly HttpClient _api;

        public PersonelPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Index() => View(); //Index.cshtml


        [HttpPost]
        public async Task<IActionResult> MusteriKaydet(MusteriDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest("Hatalar: " + string.Join(" | ", errors));
            }
            var response = await _api.PostAsJsonAsync("api/authorization/kayit", dto);
            if (response.IsSuccessStatusCode)
                return Ok("Müşteri kaydı başarılı.");
            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest("API Hatası: " + apiError);
        }

       
        [HttpPost]
        public async Task<IActionResult> MusteriGuncelle(MusteriDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model geçersiz!");

            var response = await _api.PutAsJsonAsync("api/musteri", dto);

            if (response.IsSuccessStatusCode)
                return Ok("Güncelleme başarılı!");

            var hata = await response.Content.ReadAsStringAsync();
            return BadRequest("API Hatası: " + hata);
        }
        [HttpPost]
        public async Task<IActionResult> MusteriSil(int id)
        {
            var response = await _api.DeleteAsync($"api/musteri/{id}");

            if (response.IsSuccessStatusCode)
                return Ok("Müşteri silindi.");

            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest("Silme hatası: " + apiError);
        }
        //rezervasyon
        [HttpPost]
        public async Task<IActionResult> MusteriRezervasyonEkle([FromBody] RezervasyonDTO dto)
        {
            var response = await _api.PostAsJsonAsync("api/rezervasyon", dto);
            if (response.IsSuccessStatusCode) return Ok("Rezervasyon eklendi.");
            return BadRequest("Ekleme başarısız: " + await response.Content.ReadAsStringAsync());
        }
        [HttpPost]
        public async Task<IActionResult> RezervasyonKaydet([FromBody] RezervasyonDTO dto)
        {
            if (dto.RezervasyonId == 0)
            {
                var res = await _api.PostAsJsonAsync("api/rezervasyon", dto);
                return res.IsSuccessStatusCode ? Ok("Eklendi") : BadRequest("Ekleme başarısız");
            }
            else
            {
                var res = await _api.PutAsJsonAsync($"api/rezervasyon/{dto.RezervasyonId}", dto);
                return res.IsSuccessStatusCode ? Ok("Güncellendi") : BadRequest("Güncelleme başarısız");
            }
        }

        [HttpPut]
        public async Task<IActionResult> RezervasyonIptal(int id)
        {
            var res = await _api.PutAsync($"api/rezervasyon/iptal-et/{id}", null);
            return res.IsSuccessStatusCode ? Ok("İptal edildi") : BadRequest("İptal başarısız");
        }

        //personel profil bilgileri
        [HttpPost]
        public async Task<IActionResult> ProfilGuncelle(PersonelDTO dto)
        {
            var response = await _api.PutAsJsonAsync("api/personel", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Personel_Bilgilerim");

            return BadRequest("Güncelleme başarısız.");
        }
        //şifre değiştir
        [HttpPost]
        public async Task<IActionResult> SifreDegistir(ChangePasswordDTO dto)
        {
            if (dto.YeniSifre != dto.YeniSifreTekrar)
            {
                ModelState.AddModelError("", "Yeni şifreler eşleşmiyor.");
                return RedirectToAction("Personel_Bilgilerim");
            }

            var response = await _api.PutAsJsonAsync("api/personel/sifre", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Personel_Bilgilerim");

            ModelState.AddModelError("", "Şifre güncelleme başarısız.");
            return RedirectToAction("Personel_Bilgilerim");
        }




        // Aynı şekilde: Rezervasyon ekle/güncelle işlemleri de buraya gelecek
    }
}
