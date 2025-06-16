using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using Microsoft.AspNetCore.Mvc;

public class PersonelOdaDurumlariViewComponent : ViewComponent
{
    private readonly HttpClient _api;
    public PersonelOdaDurumlariViewComponent(IHttpClientFactory httpFactory)
        => _api = httpFactory.CreateClient("ApiClient");

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var response = await _api.GetAsync("api/oda");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var opts = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        // metinsel enum’ları da parse edebilsin diye:
        opts.Converters.Add(new JsonStringEnumConverter());

        var odalar = JsonSerializer.Deserialize<List<OdaDTO>>(json, opts)
                    ?? new List<OdaDTO>();

        return View(odalar);
    }
}
