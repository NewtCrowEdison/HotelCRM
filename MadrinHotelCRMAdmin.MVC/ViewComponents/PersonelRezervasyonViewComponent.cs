//using MadrinHotelCRM.DTO.DTOModels;
//using MadrinHotelCRM.DTO;
//using Microsoft.AspNetCore.Mvc;

//namespace MadrinHotelCRMAdmin.MVC.ViewComponents
//{
//    public class PersonelRezervasyonViewComponent : ViewComponent
//    {
//        private readonly IHttpClientFactory _httpClientFactory;

//        public PersonelRezervasyonViewComponent(IHttpClientFactory factory)
//        {
//            _httpClientFactory = factory;
//        }

//        public async Task<IViewComponentResult> InvokeAsync()
//        {
//            var api = _httpClientFactory.CreateClient("ApiClient");

//            var rezervasyonlar = await api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon");
//            var odalar = await api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
//            var musteriler = await api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
//            var tarifeler = await api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife");

//            var viewModel = new RezervasyonEkleViewModel
//            {
//                RezervasyonListesi = rezervasyonlar,
//                OdaListesi = odalar,
//                MusteriListesi = musteriler,
//                TarifeListesi = tarifeler
//            };

//            return View(viewModel); // Views/Shared/Components/Rezervasyon/Default.cshtml
//        }

//    }
//}

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
            // 1) Tüm listeleri API'den çekelim
            var odalar = await _api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            var tarifeler = await _api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife");
            var musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            var rezervasyonlar = await _api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon");

            var vm = new RezervasyonEkleViewModel
            {
                YeniRezervasyon = new RezervasyonDTO(),    // <<< null referansı böyle engellemiş olacağız
                OdaListesi = odalar,
                TarifeListesi = tarifeler,
                MusteriListesi = musteriler,
                RezervasyonListesi = rezervasyonlar
            };

            return View("Default", vm);
        }
    }
}
