using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MadrinHotelCRM.DTO.DTOModels;
using System.Text.Json;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _api;

        public AuthController(IHttpClientFactory httpFactory)
            => _api = httpFactory.CreateClient("ApiClient");

        // GET: /Auth/Giris
        [HttpGet]
        public IActionResult Giris()
            => View(new GirisDTO());

        // POST: /Auth/Giris
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Giris(GirisDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // API’ye giriş isteği at
            var response = await _api.PostAsJsonAsync("api/authorization/giris", model);
            if (!response.IsSuccessStatusCode)
            {
                string hataMesaji = await response.Content.ReadAsStringAsync();

                try
                {
                    var errDict = JsonSerializer.Deserialize<Dictionary<string, string>>(hataMesaji);
                    ModelState.AddModelError(string.Empty, errDict?["message"] ?? "Giriş başarısız.");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, hataMesaji);
                }

                return View(model);
            }

            // API'den kullanıcı rolü ve ID bilgilerini al
            var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            var role = responseContent?["role"] ?? "Personel";
            var kullaniciId = responseContent?["kullaniciId"];

            // Claims ile kimlik doğrulama
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, kullaniciId)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                });

            // Session’a kayıt
            HttpContext.Session.SetString("UserEmail", model.Email);
            HttpContext.Session.SetString("UserRole", role);

            // Role’a göre yönlendirme
            return role switch
            {
                "Admin" => RedirectToAction("Index", "AdminPanel"),
                "Personel" => RedirectToAction("Index", "PersonelPanel"),
                _ => RedirectToAction("Index", "Home")
            };
        }

        // GET: /Auth/Kayit
        [HttpGet]
        public IActionResult Kayit()
            => View(new KullaniciOlusturDTO());

        // POST: /Auth/Kayit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Kayit(KullaniciOlusturDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model geçersiz.");

            var response = await _api.PostAsJsonAsync("api/authorization/kayit", model);
            if (response.IsSuccessStatusCode)
                return Ok();

            var errorContent = await response.Content.ReadAsStringAsync();
            return BadRequest("API Hatası: " + errorContent);
        }

        // POST: /Auth/Cikis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Giris");
        }
    }
}
