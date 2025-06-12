using Microsoft.AspNetCore.Mvc;

namespace MadrinHotelCRMAdmin.MVC.Controllers
{
    public class ViewComponentController : Controller
    {
        // Oda Yönetimi
        public IActionResult Odalar() => ViewComponent("Odalar");
        public IActionResult OdaTipleri() => ViewComponent("OdaTipleri");
        //public IActionResult OdaEkle() => ViewComponent("OdaEkleFormu");
        //public IActionResult OdaTipleri() => ViewComponent("OdaTipleri");
        //public IActionResult OdaTarifeleri() => ViewComponent("OdaTarifeleri");

        // Personel Yönetimi
        public IActionResult Personel() => ViewComponent("Personel"); // listeleme işlemimiz
       // public IActionResult PersonelForm() => ViewComponent("PersonelForm"); //Ekleme işlemimiz

        // Müşteri Yönetimi
        public IActionResult Musteriler() => ViewComponent("Musteriler");

        // Ek Paketler
        public IActionResult EkPaketler() => ViewComponent("EkPaketler");

        // Departmanlar
        public IActionResult Departmanlar() => ViewComponent("Departmanlar");

        // Faturalar ve Ödemeler
        public IActionResult Faturalar() => ViewComponent("Faturalar");
        public IActionResult Odemeler() => ViewComponent("Odemeler");
        public IActionResult Tarifeler() => ViewComponent("Tarifeler");

        // Geri Bildirim, Log, Takip (Eğer varsa ekleyebilirsin)
        public IActionResult SistemLoglari() => ViewComponent("SistemLoglari");
        public IActionResult GeriBildirimler() => ViewComponent("GeriBildirimler");
        public IActionResult GenelTakip() => ViewComponent("GenelTakip");

        // Personel Paneli Yetkilere göre çağrılar
        public IActionResult Personel_Musteriler() => ViewComponent("PersonelMusteriler"); 
        public IActionResult Personel_Rezervasyon() => ViewComponent("PersonelRezervasyon"); 
        public IActionResult Personel_OdaDurumlari() => ViewComponent("PersonelOdaDurumlari");
        public IActionResult Personel_Bilgilerim() => ViewComponent("PersonelKullaniciBilgileri"); 
    }
}
