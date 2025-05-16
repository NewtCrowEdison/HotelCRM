using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRM.API.Controllers
{
    public class OdaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
