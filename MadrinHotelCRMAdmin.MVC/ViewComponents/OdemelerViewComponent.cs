using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class OdemelerViewComponent : ViewComponent
    {
        private readonly IOdemeService _odemeService;

        public OdemelerViewComponent(IOdemeService odemeService)
        {
            _odemeService = odemeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var odemeler = await _odemeService.GetAllAsync();
            return View(odemeler);
        }
    }
   
}
