using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    public class OdemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
