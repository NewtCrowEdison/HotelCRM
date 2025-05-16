using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    public class RezervasyonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
