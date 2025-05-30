using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly HttpClient _api;

       
        public AdminPanelController(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        // Admin panel giriş ekranı
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        ////  Oda CRUD
        //[HttpPost]
        //public async Task<IActionResult> OdaEkle(OdaDTO model)
        //{
        //    var response = await _api.PostAsJsonAsync("api/oda", model);
        //    if (response.IsSuccessStatusCode)
        //        return Ok();
        //    return BadRequest("Oda eklenirken bir hata oluştu.");
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllOdalar()
        //{
        //    var data = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
        //    return PartialView("~/Views/_Partials/_OdalarPartial.cshtml", data);
        //}

        //[HttpPost]
        //public async Task<IActionResult> OdaSil(int id)
        //{
        //    var response = await _api.DeleteAsync($"api/oda/{id}");
        //    if (response.IsSuccessStatusCode)
        //        return Ok();

        //    return BadRequest("Oda silinemedi.");
        //}
    }
}
