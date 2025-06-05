using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class IletisimController : Controller
    {
        private readonly HttpClient _httpClient;

        public IletisimController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(IletisimFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _httpClient.PostAsJsonAsync("api/iletisim", model);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Mesaj başarıyla gönderildi!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Mesaj gönderilemedi.");
            return View(model);
        }
    }
}

