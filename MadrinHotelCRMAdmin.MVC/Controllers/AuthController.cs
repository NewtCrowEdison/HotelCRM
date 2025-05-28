// MVC/Controllers/AuthController.cs
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _api;

        public AuthController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        // GET: /Auth/Giris
        [HttpGet]
        public IActionResult Giris()
            => View(new GirisDTO());

        // POST: /Auth/Giris
        [HttpPost]
        public async Task<IActionResult> Giris(GirisDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);
            //giriş başarılıysa paneline yönlendirmek için.

            if (model.UserRole == "Admin")
                return RedirectToAction("Index", "AdminPanel");

            if (model.UserRole == "Personel")
                return RedirectToAction("Index", "PersonelPanel");


            var response = await _api.PostAsJsonAsync("api/authorization/giris", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Home");

            // Hata mesajını  ModelState’e ekleme
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            ModelState.AddModelError(string.Empty, error?["message"] ?? "Giriş başarısız.");
            return View(model);
        }

        // GET: /Auth/Kayit
        [HttpGet]
        public IActionResult Kayit()
            => View(new KullaniciOlusturDTO());

        // POST: /Auth/Kayit
        [HttpPost]
        public async Task<IActionResult> Kayit(KullaniciOlusturDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _api.PostAsJsonAsync("api/authorization/kayit", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Giris");

            var errors = await response.Content.ReadFromJsonAsync<List<IdentityError>>();
            foreach (var e in errors)
                ModelState.AddModelError(string.Empty, e.Description);

            return View(model);
        }

        // POST: /Auth/Cikis
        [HttpPost]
        public async Task<IActionResult> Cikis()
        {
            await _api.PostAsync("api/authorization/cikis", null);
            return RedirectToAction("Giris");
        }
    }
}
