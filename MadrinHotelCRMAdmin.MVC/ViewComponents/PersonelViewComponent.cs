using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.ViewComponents
{
    public class PersonelViewComponent : ViewComponent
    {
        private readonly IPersonelService _personelService;

        public PersonelViewComponent(IPersonelService personelService)
        {
            _personelService = personelService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var personeller = await _personelService.GetAllAsync();
            return View(personeller);
        }
    }
    
}
