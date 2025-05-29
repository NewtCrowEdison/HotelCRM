using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class EkPaketlerViewComponent : ViewComponent
    {
        private readonly IEkPaketService _ekPaketService;

        public EkPaketlerViewComponent(IEkPaketService ekPaketService)
        {
            _ekPaketService = ekPaketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var paketler = await _ekPaketService.GetAllAsync();
            return View(paketler);
        }
    }
   
}
