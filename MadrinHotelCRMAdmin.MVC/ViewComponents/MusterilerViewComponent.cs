using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class MusterilerViewComponent : ViewComponent
    {
        private readonly IMusteriService _musteriService;

        public MusterilerViewComponent(IMusteriService musteriService)
        {
            _musteriService = musteriService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var musteriler = await _musteriService.GetAllAsync();
            return View(musteriler);
        }
    
    }
}
