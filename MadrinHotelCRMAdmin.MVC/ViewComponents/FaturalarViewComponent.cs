using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class FaturalarViewComponent : ViewComponent
    {
        private readonly IFaturaService _faturaService;

        public FaturalarViewComponent(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var faturalar = await _faturaService.GetAllAsync();
            return View(faturalar);
        }
    }   
}
