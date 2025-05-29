// Controllers/AuthController.cs
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

            // API’ye giriş isteği atarız
            var response = await _api.PostAsJsonAsync("api/authorization/giris", model);
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                ModelState.AddModelError(string.Empty, err?["message"] ?? "Giriş başarısız.");
                return View(model);
            }

            // 2) ClaimsPrincipal oluştur ve cookie ile imzala
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  model.Email),
                new Claim(ClaimTypes.Role,  model.UserRole)
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

            // Session'da ek bilgi saklamak için
            HttpContext.Session.SetString("UserEmail", model.Email);
            HttpContext.Session.SetString("UserRole", model.UserRole);

            // Role bazlı yönlendirme yapmak için
            return model.UserRole switch
            {
                "Admin" => RedirectToAction("Index", "AdminPanel"),
                "Personel" => RedirectToAction("Index", "PersonelPanel"),
                _ => RedirectToAction("Index", "Home")
            };
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
