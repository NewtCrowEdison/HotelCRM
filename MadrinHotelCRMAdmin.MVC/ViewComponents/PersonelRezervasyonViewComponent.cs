using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class PersonelRezervasyonViewComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonelRezervasyonViewComponent(IHttpClientFactory factory)
        {
            _httpClientFactory = factory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var api = _httpClientFactory.CreateClient("ApiClient");

            var rezervasyonlar = await api.GetFromJsonAsync<List<RezervasyonDTO>>("api/rezervasyon");
            var odalar = await api.GetFromJsonAsync<List<OdaDTO>>("api/oda");
            var musteriler = await api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            var tarifeler = await api.GetFromJsonAsync<List<TarifeDTO>>("api/tarife");

            var viewModel = new RezervasyonEkleViewModel
            {
                RezervasyonListesi = rezervasyonlar,
                OdaListesi = odalar,
                MusteriListesi = musteriler,
                TarifeListesi = tarifeler
            };

            return View(viewModel); // Views/Shared/Components/Rezervasyon/Default.cshtml
        }

    }
}
