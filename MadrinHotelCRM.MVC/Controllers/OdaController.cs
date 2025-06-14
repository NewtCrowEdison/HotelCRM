using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.MVC.Controllers
{
    [Route("Odalar")]
    public class OdaController : Controller
    {
        private readonly HttpClient _api;

        public OdaController(IHttpClientFactory factory)
        {
            _api = factory.CreateClient("ApiClient");
        }

        // 1. TÜM ODALARI GETİR
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            return View(odalar);
        }

        // 2. TEK ODA DETAYI
        [HttpGet("Detay/{id}")]
        public async Task<IActionResult> Detay(int id)
        {
            var oda = await _api.GetFromJsonAsync<OdaDTO>($"api/oda/{id}");
            if (oda == null) return NotFound();
            return View(oda);
        }
    }
}
