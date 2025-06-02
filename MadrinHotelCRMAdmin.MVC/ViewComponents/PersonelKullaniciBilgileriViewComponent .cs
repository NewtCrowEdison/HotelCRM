using System.Security.Claims;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

public class PersonelKullaniciBilgileriViewComponent : ViewComponent
{
    private readonly HttpClient _httpClient;

    public PersonelKullaniciBilgileriViewComponent(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var kullaniciId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        System.Diagnostics.Debug.WriteLine(">> GİRİŞ YAPAN ID: " + kullaniciId); // log için

        if (string.IsNullOrEmpty(kullaniciId))
            return Content("Giriş bilgisi bulunamadı");

        var response = await _httpClient.GetAsync($"api/personel/kullanici/{kullaniciId}");

        if (!response.IsSuccessStatusCode)
            return Content("Bilgiler alınamadı");

        var personel = await response.Content.ReadFromJsonAsync<PersonelDTO>();
        return View(personel);
    }
}

