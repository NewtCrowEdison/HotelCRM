using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class PersonelMusterilerViewComponent : ViewComponent
    {
        private readonly HttpClient _api;

        public PersonelMusterilerViewComponent(IHttpClientFactory factory)
        {
            _api = factory.CreateClient("ApiClient");
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MusteriDTO>? musteriListesi;

            try
            {
                musteriListesi = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            }
            catch
            {
                musteriListesi = new(); // Hata olursa boş liste
            }

            var model = new MusteriEkleViewModel
            {
                MusteriListesi = musteriListesi ?? new List<MusteriDTO>(),
               
            };

            return View(model);
        }
    }
}
