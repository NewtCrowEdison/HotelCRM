using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly HttpClient _api;
        //base adresi çekebilmek için IHttpClientFactory kullanıyoruz
        public AdminPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        //  Oda Yönetimi
        public IActionResult Odalar() => PartialView("~/Views/_Partials/_OdalarPartial.cshtml");
        public IActionResult OdaTipleri() => PartialView("~/Views/_Partials/_OdaTipleriPartial.cshtml");
        public IActionResult OdaTarifeleri() => PartialView("~/Views/_Partials/_OdaTarifeleriPartial.cshtml");

        //  Personel Yönetimi
        public IActionResult Personel() => PartialView("~/Views/_Partials/_PersonelPartial.cshtml");
        public IActionResult Departmanlar() => PartialView("~/Views/_Partials/_DepartmanlarPartial.cshtml");

        //  Müşteri ve Etiket
        public IActionResult Musteriler() => PartialView("~/Views/_Partials/_MusterilerPartial.cshtml");
        public IActionResult Etiketler() => PartialView("~/Views/_Partials/_EtiketlerPartial.cshtml");

        //  Ek Paketler
        public IActionResult EkPaketler() => PartialView("~/Views/_Partials/_EkPaketlerPartial.cshtml");

        //  Fatura ve Ödeme
        public IActionResult Faturalar() => PartialView("~/Views/_Partials/_FaturalarPartial.cshtml");
        public IActionResult Odemeler() => PartialView("~/Views/_Partials/_OdemelerPartial.cshtml");
        public IActionResult Tarifeler() => PartialView("~/Views/_Partials/_TarifelerPartial.cshtml");

        //  Sistem ve Bildirim
        public IActionResult SistemLoglari() => PartialView("~/Views/_Partials/_SistemLoglariPartial.cshtml");
        public IActionResult GeriBildirimler() => PartialView("~/Views/_Partials/_GeriBildirimlerPartial.cshtml");
        public IActionResult GenelTakip() => PartialView("~/Views/_Partials/_GenelTakipPartial.cshtml");

        //  Oda CRUD 

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

    }

}

