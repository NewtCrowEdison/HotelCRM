using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class DepartmanlarViewComponent : ViewComponent
    {
        private readonly IDepartmanService _departmanService;

        public DepartmanlarViewComponent(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _departmanService.GetAllAsync();
            return View(data);
        }
    }
    
}
