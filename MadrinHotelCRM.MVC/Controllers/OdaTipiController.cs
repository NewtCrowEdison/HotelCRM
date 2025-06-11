using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class OdaTipiController : Controller
    {
        private readonly HttpClient _httpClient;

        public OdaTipiController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5062/"); // API adresi
        }

        // GET: /OdaTipi
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/odatipi");
            if (!response.IsSuccessStatusCode)
                return View(new List<OdaTipiDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<OdaTipiDTO>>(json);
            return View(list);
        }

        // GET: /OdaTipi/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/odatipi/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<OdaTipiDTO>(json);
            return View(dto);
        }

        // GET: /OdaTipi/Create
        public IActionResult Create() => View();
       
        // POST: /OdaTipi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OdaTipiDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/odatipi", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Oluşturma sırasında bir hata oluştu.");
            return View(dto);
        }

        // GET: /OdaTipi/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/odatipi/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<OdaTipiDTO>(json);
            return View(dto);
        }

        // POST: /OdaTipi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OdaTipiDTO dto)
        {
            if (id != dto.OdaTipiId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/odatipi/{id}", content);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Güncelleme sırasında bir hata oluştu.");
            return View(dto);
        }

        // GET: /OdaTipi/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/odatipi/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<OdaTipiDTO>(json);
            return View(dto);
        }

        // POST: /OdaTipi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/odatipi/{id}");
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            TempData["Error"] = "Silme işlemi başarısız oldu.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        // GET: /OdaTipi/FilterByCapacity?kapasite=2
        public async Task<IActionResult> FilterByCapacity(int kapasite)
        {
            var response = await _httpClient.GetAsync($"api/odatipi/kapasite/{kapasite}");
            if (!response.IsSuccessStatusCode)
                return View(new List<OdaTipiDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<OdaTipiDTO>>(json);
            ViewBag.Kapasite = kapasite;
            return View(list);
        }
    }
}
