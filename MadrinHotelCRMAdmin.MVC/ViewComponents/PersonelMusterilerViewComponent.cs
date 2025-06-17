using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class PersonelMusterilerViewComponent : ViewComponent
    {
        private readonly HttpClient _api;

        public PersonelMusterilerViewComponent(IHttpClientFactory httpFactory)
        {
            _api = httpFactory.CreateClient("ApiClient");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MusteriDTO> musteriler;
            try
            {
                musteriler = await _api.GetFromJsonAsync<List<MusteriDTO>>("api/musteri");
            }
            catch
            {
                musteriler = new List<MusteriDTO>(); // API hatası durumunda boş liste
            }

            var model = new MusteriEkleViewModel
            {
                MusteriListesi = musteriler,
                // Gerekirse diğer ViewModel alanlarını da burada doldurun
            };

            return View(model);
        }
    }
}