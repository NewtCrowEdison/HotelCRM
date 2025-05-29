using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class OdalarViewComponent : ViewComponent
    {
        private readonly IOdaService _odaService;

        public OdalarViewComponent(IOdaService odaService)
        {
            _odaService = odaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var odalar = await _odaService.GetAllAsync();
            return View(odalar);
        }
    }
  
}
