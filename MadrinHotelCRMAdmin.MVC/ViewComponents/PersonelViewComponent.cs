using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

public class PersonelViewComponent : ViewComponent
{
    private readonly HttpClient _api;

    public PersonelViewComponent(IHttpClientFactory factory)
    {
        _api = factory.CreateClient("ApiClient");
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<PersonelDTO>? personelListesi;

        try
        {
            personelListesi = await _api.GetFromJsonAsync<List<PersonelDTO>>("api/personel");
        }
        catch
        {
            personelListesi = new(); // Hata olursa boş liste
        }

        var model = new PersonelEkleViewModel
        {
            PersonelListesi = personelListesi ?? new List<PersonelDTO>(),
            Kullanici = new KullaniciOlusturDTO { Rol = "Personel" }
        };

        return View(model);
    }
}
