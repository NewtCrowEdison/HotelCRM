using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class MusteriController : Controller
    {
        private readonly HttpClient _httpClient;

        public MusteriController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5062/"); // API adresi
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/musteri");

            if (!response.IsSuccessStatusCode)
                return View(new List<MusteriDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var musteriler = JsonConvert.DeserializeObject<List<MusteriDTO>>(json);

            return View(musteriler);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/musteri/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var musteri = JsonConvert.DeserializeObject<MusteriDTO>(json);

            return View(musteri);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MusteriDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/musteri", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"api/musteri/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var musteri = JsonConvert.DeserializeObject<MusteriDTO>(json);

            return View(musteri);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MusteriDTO dto)
        {
            if (id != dto.MusteriId)
                return BadRequest();

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/musteri/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/musteri/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var musteri = JsonConvert.DeserializeObject<MusteriDTO>(json);

            return View(musteri);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/musteri/{id}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return RedirectToAction("Delete", new { id });
        }

        // Etiket Atama
        [HttpPost]
        public async Task<IActionResult> AssignTag(int musteriId, int etiketId)
        {
            var response = await _httpClient.PostAsync($"api/musteri/{musteriId}/AssignTag/{etiketId}", null);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Details", new { id = musteriId });

            TempData["Error"] = "Etiket atama başarısız.";
            return RedirectToAction("Details", new { id = musteriId });
        }

        // Etiket Kaldırma
        [HttpPost]
        public async Task<IActionResult> RemoveTag(int musteriId, int etiketId)
        {
            var response = await _httpClient.DeleteAsync($"api/musteri/{musteriId}/RemoveTag/{etiketId}");

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Details", new { id = musteriId });

            TempData["Error"] = "Etiket kaldırma başarısız.";
            return RedirectToAction("Details", new { id = musteriId });
        }

        // Etkileşimler
        public async Task<IActionResult> Interactions(int musteriId)
        {
            var response = await _httpClient.GetAsync($"api/musteri/{musteriId}/interactions");

            if (!response.IsSuccessStatusCode)
                return View(new List<MusteriEtkilesimDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var interactions = JsonConvert.DeserializeObject<List<MusteriEtkilesimDTO>>(json);

            return View(interactions);
        }

        // Geri Bildirimler
        public async Task<IActionResult> Feedbacks(int musteriId)
        {
            var response = await _httpClient.GetAsync($"api/musteri/{musteriId}/feedbacks");

            if (!response.IsSuccessStatusCode)
                return View(new List<GeriBildirimDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var feedbacks = JsonConvert.DeserializeObject<List<GeriBildirimDTO>>(json);

            return View(feedbacks);
        }
    }
}
