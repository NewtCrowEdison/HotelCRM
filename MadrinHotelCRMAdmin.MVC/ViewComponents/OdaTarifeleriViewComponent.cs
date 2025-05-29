using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class OdaTarifeleriViewComponent : ViewComponent
    {
        private readonly IOdaTarifeService _odaTarifeService;

        public OdaTarifeleriViewComponent(IOdaTarifeService odaTarifeService)
        {
            _odaTarifeService = odaTarifeService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tarifeler = await _odaTarifeService.GetAllAsync();
            return View(tarifeler);
        }
    
    }
}
