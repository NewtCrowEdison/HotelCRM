using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class TarifelerViewComponent : ViewComponent
    {
        private readonly ITarifeService _tarifeService;

        public TarifelerViewComponent(ITarifeService tarifeService)
        {
            _tarifeService = tarifeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tarifeler = await _tarifeService.GetAllAsync();
            return View(tarifeler);
        }
    }
    
}
