using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class OdaTipleriViewComponent : ViewComponent
    {
        private readonly IOdaTipiService _odaTipiService;

        public OdaTipleriViewComponent(IOdaTipiService odaTipiService)
        {
            _odaTipiService = odaTipiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var odaTipleri = await _odaTipiService.GetAllAsync();
            return View(odaTipleri);
        }
    }
   
}
