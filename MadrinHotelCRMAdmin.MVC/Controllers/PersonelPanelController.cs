using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class PersonelPanelController : Controller
    {
        private readonly HttpClient _api;

        public PersonelPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        // Ana sayfa
        [HttpGet]
        public IActionResult Index() => View();

        //Oda Durumları ViewComponent çağrısı
        [HttpGet]
        public IActionResult OdaDurumlari()
            => ViewComponent("PersonelOdaDurumlari");


        // --- MÜŞTERİ İŞLEMLERİ ---

        // POST: /PersonelPanel/MusteriKaydet
        [HttpPost]
        public async Task<IActionResult> MusteriKaydet([FromBody] MusteriDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                               .SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage);
                return BadRequest($"Hatalar: {string.Join(" | ", errors)}");
            }

            // doğrudan api/musteri endpoint’ine POST
            var response = await _api.PostAsJsonAsync("api/musteri", dto);
            if (response.IsSuccessStatusCode)
            {
                var created = await response.Content.ReadFromJsonAsync<MusteriDTO>();
                return Ok(created);
            }

            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest($"API Hatası: {apiError}");
        }

        [HttpPost]
        public async Task<IActionResult> MusteriGuncelle([FromBody] MusteriDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model geçersiz!");

            // PUT api/musteri/{id}
            var response = await _api.PutAsJsonAsync($"api/musteri/{dto.MusteriId}", dto);
            if (response.IsSuccessStatusCode)
                return Ok("Güncelleme başarılı!");

            var hata = await response.Content.ReadAsStringAsync();
            return BadRequest($"API Hatası: {hata}");
        }

        [HttpPost]
        public async Task<IActionResult> MusteriSil([FromBody] int id)
        {
            var response = await _api.DeleteAsync($"api/musteri/{id}");
            if (response.IsSuccessStatusCode)
                return Ok("Müşteri silindi.");

            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest($"Silme hatası: {apiError}");
        }


        // --- REZERVASYON İŞLEMLERİ ---
        [HttpPost]
        public async Task<IActionResult> MusteriRezervasyonEkle([FromBody] RezervasyonDTO dto)
        {
            var response = await _api.PostAsJsonAsync("api/rezervasyon", dto);
            return response.IsSuccessStatusCode
                ? Ok("Rezervasyon eklendi.")
                : BadRequest($"Ekleme başarısız: {await response.Content.ReadAsStringAsync()}");
        }
        [HttpPost]
        public async Task<IActionResult> RezervasyonKaydet([FromBody] RezervasyonDTO dto)
        {
            HttpResponseMessage res;
            if (dto.RezervasyonId == 0)
                res = await _api.PostAsJsonAsync("api/rezervasyon", dto);
            else
                res = await _api.PutAsJsonAsync($"api/rezervasyon/{dto.RezervasyonId}", dto);

            return res.IsSuccessStatusCode
                ? Ok(dto.RezervasyonId == 0 ? "Eklendi" : "Güncellendi")
                : BadRequest(dto.RezervasyonId == 0 ? "Ekleme başarısız" : "Güncelleme başarısız");
        }

        [HttpPut]
        public async Task<IActionResult> RezervasyonIptal(int id)
        {
            var res = await _api.PutAsync($"api/rezervasyon/iptal-et/{id}", null);
            return res.IsSuccessStatusCode
                ? Ok("İptal edildi")
                : BadRequest("İptal başarısız");
        }


        // --- PERSONEL PROFİL İŞLEMLERİ ---
        [HttpPost]
        public async Task<IActionResult> ProfilGuncelle(PersonelDTO dto)
        {
            var response = await _api.PutAsJsonAsync("api/personel", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Personel_Bilgilerim");
            return BadRequest("Güncelleme başarısız.");
        }

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


        [HttpGet]
        public async Task<IActionResult> OdaRezervasyonForm(int odaId)
        {
            var tarifeler = await _api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife");
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            var rezervasyonlar = await _api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon");
            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            var secilenOda = odalar.FirstOrDefault(o => o.OdaId == odaId);

            var vm = new RezervasyonEkleViewModel
            {
                YeniRezervasyon = new RezervasyonDTO { OdaId = odaId },
                TarifeListesi = tarifeler,
                MusteriListesi = musteriler,
                RezervasyonListesi = rezervasyonlar,
                OdaListesi = new List<OdaDTO> { secilenOda }
            };

            return PartialView("PersonelRezervasyon", vm);
        }
    }
}
