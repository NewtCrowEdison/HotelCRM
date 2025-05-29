using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class ViewComponentController : Controller
    {
        public IActionResult Odalar()
        {
            return ViewComponent("Odalar");
        }
        public IActionResult OdaTipleri()
        {
            return ViewComponent("OdaTipleri");
        }
        public IActionResult Personel()
        {
            return ViewComponent("Personel");
        }
        public IActionResult EkPaketler()
        {
            return ViewComponent("EkPaketler");
        }
        public IActionResult Faturalar()
        {
            return ViewComponent("Faturalar");
        }
        public IActionResult Departmanlar()
        {
            return ViewComponent("Departmanlar");
        }
        public IActionResult Musteriler()
        {
            return ViewComponent("Musteriler");
        }
        public IActionResult OdaTarifeleri()
        {
            return ViewComponent("OdaTarifeleri");
        }
        public IActionResult Odemeler()
        {
            return ViewComponent("Odemeler");
        }
        public IActionResult Tarifeler()
        {
            return ViewComponent("Tarifeler");
        }

    }
}
