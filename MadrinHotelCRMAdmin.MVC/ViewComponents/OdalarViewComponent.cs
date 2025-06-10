using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MadrinHotelCRM.MVC.ViewComponents
{
    /// <summary>
    /// Oda yönetimi ViewComponent: odaları listeler ve form/dropdown verilerini hazırlar.
    /// </summary>
    public class OdalarViewComponent : ViewComponent
    {
        private readonly IOdaService _odaService;
        private readonly IOdaTipiService _odaTipiService;

        public OdalarViewComponent(
            IOdaService odaService,
            IOdaTipiService odaTipiService)
        {
            _odaService = odaService;
            _odaTipiService = odaTipiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                // 1) Tüm odaları al (listeleme için)
                var odalar = (await _odaService.GetAllAsync()).ToList();

                // 2) Oda tiplerini al (dropdown için)
                var tipler = await _odaTipiService.GetAllAsync();
                var odaTipiList = tipler
                    .Select(t => new SelectListItem
                    {
                        Value = t.OdaTipiId.ToString(),
                        Text = t.OdaTurAd
                    })
                    .ToList();

                // 3) Enum OdaDurum için durum listesini hazırla
                var durumlar = Enum.GetValues(typeof(OdaDurum))
                    .Cast<OdaDurum>()
                    .Select(d => new SelectListItem
                    {
                        Value = ((int)d).ToString(),
                        Text = d.GetType()
                            .GetField(d.ToString())
                            ?.GetCustomAttributes(typeof(DisplayAttribute), false)
                            .Cast<DisplayAttribute>()
                            .FirstOrDefault()?.Name
                            ?? d.ToString()
                    })
                    .ToList();

                // ViewBag ile view'a aktar
                ViewBag.OdaTipiList = odaTipiList;
                ViewBag.Durumlar = durumlar;

                return View(odalar);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj göster
                var errorHtml =
                    $"<div class='alert alert-danger'><strong>ViewComponent Hatası:</strong> {ex.Message}</div>";
                return Content(errorHtml);
            }
        }
    }
}
