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
        public IActionResult Index()
        {
            ViewBag.ApiBase = _api.BaseAddress.ToString().TrimEnd('/');
            return View();
        }

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

            // Var olan müşteri listesini API'den çek
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");

            bool ayniTcVar = !string.IsNullOrWhiteSpace(dto.TcNo) &&
                             musteriler.Any(m => m.TcNo == dto.TcNo);

            bool ayniEmailVar = !string.IsNullOrWhiteSpace(dto.Email) &&
                                musteriler.Any(m => m.Email.ToLower() == dto.Email.ToLower());

            if (ayniTcVar)
                return BadRequest("Bu TC Kimlik numarası ile kayıtlı bir müşteri zaten var.");

            if (ayniEmailVar)
                return BadRequest("Bu Email adresi ile kayıtlı bir müşteri zaten var.");

            // POST api/musteri
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

            if (!res.IsSuccessStatusCode)
            {
                // API tarafındaki gerçek hata
                var apiError = await res.Content.ReadAsStringAsync();
                return BadRequest(apiError);
            }

            return Ok(dto.RezervasyonId == 0 ? "Eklendi" : "Güncellendi");
        }


        [HttpPut]
        public async Task<IActionResult> RezervasyonIptal(int id, [FromBody] string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return BadRequest("İptal nedeni gerekli.");

            // Query string ile reason gönderiyoruz
            var response = await _api.PutAsync(
                $"api/rezervasyon/iptal-et/{id}?reason={System.Net.WebUtility.UrlEncode(reason)}",
                null
            );

            if (!response.IsSuccessStatusCode)
                return BadRequest(await response.Content.ReadAsStringAsync());

            return Ok();
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
        public async Task<IActionResult> OdaRezervasyonForm()
        {
            var tarifeler = await _api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife");
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            var rezervasyonlar = await _api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon");
            // İşte burası değişti:
            var bosOdalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda/bos");
            var faturalar = await _api.GetFromJsonAsync<List<FaturaDTO>>("api/fatura");


            var vm = new RezervasyonEkleViewModel
            {
                YeniRezervasyon = new RezervasyonDTO(),  
                TarifeListesi = tarifeler,
                MusteriListesi = musteriler,
                RezervasyonListesi = rezervasyonlar,
                OdaListesi = bosOdalar,
                FaturaListesi = faturalar
            };

            return ViewComponent("PersonelRezervasyon", vm);
        }

        [HttpGet]
        public async Task<IActionResult> PersonelOdaDurumlari()
        {
            // Tüm odaları çek
            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            return ViewComponent("PersonelOdaDurumlari", odalar);
        }


        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> FaturaOlustur(int id)
        {
            var response = await _api.PostAsync($"api/fatura/olustur/{id}", null);
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return BadRequest($"Fatura oluşturulamadı: {err}");
            }
            return Ok("Fatura başarıyla oluşturuldu!");
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]  
        public async Task<IActionResult> FaturaSil(int id)
        {
            // API DELETE isteği
            var response = await _api.DeleteAsync($"api/fatura/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return BadRequest($"Silme başarısız: {err}");
            }
            return Ok();
        }

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> FaturaDurumGuncelle(int id)
        {
            var response = await _api.PostAsync(
                $"api/fatura/durum-guncelle/{id}?yeniDurum=Odenen",
                null
            );
            if (!response.IsSuccessStatusCode)
                return BadRequest(await response.Content.ReadAsStringAsync());

            return Ok();
        }

    }
}
