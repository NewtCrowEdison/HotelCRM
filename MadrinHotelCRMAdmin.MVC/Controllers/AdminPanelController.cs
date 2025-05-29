using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly HttpClient _api;

        public AdminPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        // ---------------------- PANEL GÖVDE ---------------------- //
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // ---------------------- PERSONEL ---------------------- //
        [HttpGet]
        public async Task<IActionResult> Personel()
        {
            var liste = await _api.GetFromJsonAsync<List<PersonelDTO>>("api/personel");
            return PartialView("~/Views/_Partials/_PersonelPartial.cshtml", liste);
        }

        // Personel + Kullanıcı Oluşturma
        [HttpGet]
        public async Task<IActionResult> PersonelKayitFormu()
        {
            var personeller = await _api.GetFromJsonAsync<List<PersonelDTO>>("api/personel");
            var model = new Tuple<KullaniciOlusturDTO, List<PersonelDTO>>(new KullaniciOlusturDTO(), personeller);
            return PartialView("~/Views/_Partials/_PersonelKayitPartial.cshtml", model);
        }


        //  Personel Kaydetme 
        [HttpPost]
        public async Task<IActionResult> PersonelKayit(KullaniciOlusturDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                return BadRequest("Hatalar: " + string.Join(" | ", errors));
            }

            var response = await _api.PostAsJsonAsync("api/authorization/kayit", model);

            if (response.IsSuccessStatusCode)
                return Ok("Personel kaydı ve kullanıcı oluşturma başarılı.");

            var apiError = await response.Content.ReadAsStringAsync();
            return BadRequest("API Hatası: " + apiError);
        }

        [HttpPost]
        public async Task<IActionResult> PersonelSil(int id)
        {
            var response = await _api.DeleteAsync($"api/personel/{id}");
            if (response.IsSuccessStatusCode)
                return Ok();
            return BadRequest("Personel silinemedi.");
        }

        // ---------------------- ODA ---------------------- //
        public IActionResult Odalar() => PartialView("~/Views/_Partials/_OdalarPartial.cshtml");
        public IActionResult OdaTipleri() => PartialView("~/Views/_Partials/_OdaTipleriPartial.cshtml");
        public IActionResult OdaTarifeleri() => PartialView("~/Views/_Partials/_OdaTarifeleriPartial.cshtml");

        [HttpPost]
        public async Task<IActionResult> OdaEkle(OdaDTO model)
        {
            var response = await _api.PostAsJsonAsync("api/oda", model);
            if (response.IsSuccessStatusCode)
                return Ok();
            return BadRequest("Oda eklenirken bir hata oluştu.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOdalar()
        {
            var data = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            return PartialView("~/Views/_Partials/_OdalarPartial.cshtml", data);
        }

        [HttpPost]
        public async Task<IActionResult> OdaSil(int id)
        {
            var response = await _api.DeleteAsync($"api/oda/{id}");
            if (response.IsSuccessStatusCode)
                return Ok();
            return BadRequest("Oda silinemedi.");
        }

        // ---------------------- MÜŞTERİ - ETİKET ---------------------- //
        public IActionResult Musteriler() => PartialView("~/Views/_Partials/_MusterilerPartial.cshtml");
        public IActionResult Etiketler() => PartialView("~/Views/_Partials/_EtiketlerPartial.cshtml");

        // ---------------------- FATURA - ÖDEME ---------------------- //
        public IActionResult Faturalar() => PartialView("~/Views/_Partials/_FaturalarPartial.cshtml");
        public IActionResult Odemeler() => PartialView("~/Views/_Partials/_OdemelerPartial.cshtml");
        public IActionResult Tarifeler() => PartialView("~/Views/_Partials/_TarifelerPartial.cshtml");

        // ---------------------- SİSTEM BİLDİRİM ---------------------- //
        public IActionResult SistemLoglari() => PartialView("~/Views/_Partials/_SistemLoglariPartial.cshtml");
        public IActionResult GeriBildirimler() => PartialView("~/Views/_Partials/_GeriBildirimlerPartial.cshtml");
        public IActionResult GenelTakip() => PartialView("~/Views/_Partials/_GenelTakipPartial.cshtml");
    }
}
