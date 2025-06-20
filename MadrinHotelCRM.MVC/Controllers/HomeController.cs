using MadrinHotelCRM.MVC.Models;   
using MadrinHotelCRM.DTO.DTOModels;  
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _api;

        public HomeController(
            ILogger<HomeController> logger,
            IHttpClientFactory httpFactory)
        {
            _logger = logger;
            _api = httpFactory.CreateClient("ApiClient");
        }

        public IActionResult Index() => View();

        public IActionResult Hakkimizda() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

   
        //  M��TER� KAYDETME AKS�YONU
  
        [HttpGet]
        public IActionResult MusteriKaydet()
          => View(new MusteriDTO());
       
        [HttpPost]
        public async Task<IActionResult> MusteriKaydet(MusteriDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Hatalar� View�a g�ndersin isterseniz:
                return View("Index", model);
            }

            var response = await _api.PostAsJsonAsync("api/musteri", model);
            if (response.IsSuccessStatusCode)
            {
                var created = await response.Content.ReadFromJsonAsync<MusteriDTO>();
                // M��teri olu�turulduktan sonra �rne�in Rezervasyon sayfas�na y�nlendirebilirsiniz:
                return RedirectToAction("Rezervasyon", new { musteriId = created.MusteriId });
            }

            // API hatas�n� kullan�c�ya g�ster
            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Hatas�: {error}");
            return View("Index", model);
        }

        // REZERVASYON KAYDETME AKSIYONU
        [HttpPost]
        public async Task<IActionResult> RezervasyonKaydet(RezervasyonDTO model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Rezervasyon");

            // Geçmiş tarih kontrolü
            var today = DateTime.Today;

            if (model.BaslangicTarihi.Date < today || model.BitisTarihi.Date < today)
            {
                TempData["Hata"] = "Geçmiş tarihli rezervasyon yapılamaz.";
                return RedirectToAction("Rezervasyon");
            }

            if (model.BitisTarihi < model.BaslangicTarihi)
            {
                TempData["Hata"] = "Bitiş tarihi, başlangıç tarihinden önce olamaz.";
                return RedirectToAction("Rezervasyon");
            }

            HttpResponseMessage resp = model.RezervasyonId == 0
                ? await _api.PostAsJsonAsync("api/rezervasyon", model)
                : await _api.PutAsJsonAsync($"api/rezervasyon/{model.RezervasyonId}", model);

            if (resp.IsSuccessStatusCode)
                return RedirectToAction("RezervasyonSonuc");

            // Hata durumunda detaylı mesaj göster
            var apiErr = await resp.Content.ReadAsStringAsync();
            TempData["Hata"] = $"Rezervasyon başarısız: {apiErr}";
            return RedirectToAction("Rezervasyon");
        }



        //  REZERVASYON FORMU G�STERME
        [HttpGet]
        public async Task<IActionResult> Rezervasyon(int? musteriId)
        {
            //  T�m odalar� �ek (api/oda)
            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda")
                         ?? new List<OdaDTO>();

            // Tarifeleri �ek (api/tarife)
            var tarifeler = await _api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife")
                            ?? new List<TarifeDTO>();

            // T�m rezervasyonlar� �ek (api/rezervasyon) ve iptal edilmemi�leri filtrele
            var tumRezervasyonlar = await _api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon")
                                      ?? new List<RezervasyonDTO>();
            var aktifRezervasyonlar = tumRezervasyonlar
                .Where(r => r.IptalTarihi == null)
                .ToList();

            // M��terileri �ek (api/musteri)
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri")
                            ?? new List<MusteriDTO>();

            // ViewBag�e doldur
            ViewBag.Odalar = odalar;
            ViewBag.Tarifeler = tarifeler;
            ViewBag.RezervasyonListesi = aktifRezervasyonlar;
            ViewBag.MusteriListesi = musteriler;

            // View�a g�nderilecek model (opsiyonel default de�erler)
            var model = new RezervasyonDTO
            {
                MusteriId = musteriId ?? 0,
                BaslangicTarihi = DateTime.Today,
                BitisTarihi = DateTime.Today.AddDays(1)
            };

            return View(model);
        }

        //  REZERVASYON SONU� SAYFASI
  
        public IActionResult RezervasyonSonuc()
        {
            return View();
        }

    }
}
