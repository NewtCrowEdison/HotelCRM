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

   
        //  MÜÞTERÝ KAYDETME AKSÝYONU
  
        [HttpGet]
        public IActionResult MusteriKaydet()
          => View(new MusteriDTO());
       
        [HttpPost]
        public async Task<IActionResult> MusteriKaydet(MusteriDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Hatalarý View’a göndersin isterseniz:
                return View("Index", model);
            }

            var response = await _api.PostAsJsonAsync("api/musteri", model);
            if (response.IsSuccessStatusCode)
            {
                var created = await response.Content.ReadFromJsonAsync<MusteriDTO>();
                // Müþteri oluþturulduktan sonra örneðin Rezervasyon sayfasýna yönlendirebilirsiniz:
                return RedirectToAction("Rezervasyon", new { musteriId = created.MusteriId });
            }

            // API hatasýný kullanýcýya göster
            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"API Hatasý: {error}");
            return View("Index", model);
        }

        //  REZERVASYON KAYDETME AKSÝYONU
        [HttpPost]
        public async Task<IActionResult> RezervasyonKaydet(RezervasyonDTO model)
        {
            if (!ModelState.IsValid)
            {
                // Rezervasyon formunuz varsa ona dönün:
                return RedirectToAction("Rezervasyon");
            }

            // Eðer RezervasyonId = 0 ise yeni, deðilse güncelle
            HttpResponseMessage resp = model.RezervasyonId == 0
                ? await _api.PostAsJsonAsync("api/rezervasyon", model)
                : await _api.PutAsJsonAsync($"api/rezervasyon/{model.RezervasyonId}", model);

            if (resp.IsSuccessStatusCode)
            {
                // Baþarýyla kaydedildi
                return RedirectToAction("RezervasyonSonuc");
            }

            // Hata varsa
            var apiErr = await resp.Content.ReadAsStringAsync();
            ModelState.AddModelError("", $"Rezervasyon baþarýsýz: {apiErr}");
            return RedirectToAction("Rezervasyon");
        }

      
        //  REZERVASYON FORMU GÖSTERME
        [HttpGet]
        public async Task<IActionResult> Rezervasyon(int? musteriId)
        {
            //  Tüm odalarý çek (api/oda)
            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda")
                         ?? new List<OdaDTO>();

            // Tarifeleri çek (api/tarife)
            var tarifeler = await _api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife")
                            ?? new List<TarifeDTO>();

            // Tüm rezervasyonlarý çek (api/rezervasyon) ve iptal edilmemiþleri filtrele
            var tumRezervasyonlar = await _api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon")
                                      ?? new List<RezervasyonDTO>();
            var aktifRezervasyonlar = tumRezervasyonlar
                .Where(r => r.IptalTarihi == null)
                .ToList();

            // Müþterileri çek (api/musteri)
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri")
                            ?? new List<MusteriDTO>();

            // ViewBag’e doldur
            ViewBag.Odalar = odalar;
            ViewBag.Tarifeler = tarifeler;
            ViewBag.RezervasyonListesi = aktifRezervasyonlar;
            ViewBag.MusteriListesi = musteriler;

            // View’a gönderilecek model (opsiyonel default deðerler)
            var model = new RezervasyonDTO
            {
                MusteriId = musteriId ?? 0,
                BaslangicTarihi = DateTime.Today,
                BitisTarihi = DateTime.Today.AddDays(1)
            };

            return View(model);
        }

        //  REZERVASYON SONUÇ SAYFASI
  
        public IActionResult RezervasyonSonuc()
        {
            return View();
        }
    }
}
