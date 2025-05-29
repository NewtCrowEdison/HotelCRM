using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class AdminPanelController : Controller
    {
        // Sadece Index sayfasını açacak. CRUD işlemleri  API vw AJAX ile yapılcak
        public IActionResult Index()
        {
            return View();
        }

    }

}

