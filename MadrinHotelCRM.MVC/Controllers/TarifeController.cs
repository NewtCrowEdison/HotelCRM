using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class TarifeController : Controller
    {
        private readonly HttpClient _httpClient;

        public TarifeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5062/"); // API adresi
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/tarife");

            if (!response.IsSuccessStatusCode)
                return View(new List<TarifeDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var tarifeler = JsonConvert.DeserializeObject<List<TarifeDTO>>(json);

            return View(tarifeler);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/tarife/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var tarife = JsonConvert.DeserializeObject<TarifeDTO>(json);

            return View(tarife);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TarifeDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/tarife", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/tarife/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var tarife = JsonConvert.DeserializeObject<TarifeDTO>(json);

            return View(tarife);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TarifeDTO dto)
        {
            if (id != dto.TarifeId)
                return BadRequest();

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/tarife/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/tarife/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var tarife = JsonConvert.DeserializeObject<TarifeDTO>(json);

            return View(tarife);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/tarife/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return RedirectToAction("Delete", new { id });
        }

        // İndirim uygula
        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(int tarifeId, decimal discountRate)
        {
            var response = await _httpClient.PutAsync($"api/tarife/{tarifeId}/uygula-indirim?discountRate={discountRate}", null);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Details", new { id = tarifeId });

            TempData["Error"] = "İndirim uygulama başarısız.";
            return RedirectToAction("Details", new { id = tarifeId });
        }

        // İndirimli tarifeleri listele
        public async Task<IActionResult> DiscountedTariffs()
        {
            var response = await _httpClient.GetAsync("api/tarife/indirimli-tarifeler");

            if (!response.IsSuccessStatusCode)
                return View(new List<TarifeDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var tarifeler = JsonConvert.DeserializeObject<List<TarifeDTO>>(json);

            return View(tarifeler);
        }

        // Belirli oda için tarifeleri getir
        public async Task<IActionResult> RoomTariffs(int odaId)
        {
            var response = await _httpClient.GetAsync($"api/tarife/oda-tarifeleri/{odaId}");

            if (!response.IsSuccessStatusCode)
                return View(new List<TarifeDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var odaTarifeler = JsonConvert.DeserializeObject<List<TarifeDTO>>(json);

            return View(odaTarifeler);
        }
    }
}
