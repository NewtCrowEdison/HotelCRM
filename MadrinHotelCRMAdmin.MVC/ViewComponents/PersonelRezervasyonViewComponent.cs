using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class PersonelRezervasyonViewComponent : ViewComponent
    {
        private readonly HttpClient _api;
        public PersonelRezervasyonViewComponent(IHttpClientFactory factory)
            => _api = factory.CreateClient("ApiClient");

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            var tarifeler = await _api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife");
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            var rezervasyonlar = await _api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon");
            var faturalar = await _api.GetFromJsonAsync<List<FaturaDTO>>("api/fatura");

            var vm = new RezervasyonEkleViewModel
            {
                YeniRezervasyon = new RezervasyonDTO(),
                OdaListesi = odalar,
                TarifeListesi = tarifeler,
                MusteriListesi = musteriler,
                RezervasyonListesi = rezervasyonlar,
                FiltreBaslangic = null,
                FiltreBitis = null,
                FaturaListesi = faturalar
            };

            return View("Default", vm);
        }
    }
}
