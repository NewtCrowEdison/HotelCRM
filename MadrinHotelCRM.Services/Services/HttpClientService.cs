using MadrinHotelCRM.Services.Interfaces;
using System.Net.Http.Json;

namespace MadrinHotelCRM.Services.Services
{
    /// <summary>
    /// HttpClientService, IHttpClientService'i uygular ve IHttpClientFactory kullanarak HTTP istekleri gönderir.
    /// IHttpClientFactory kullanımı, HttpClient yaşam döngüsünü yönetir, performansı artırır ve bağlantı sızıntılarını önler.
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Belirtilen URL'ye GET isteği gönderir ve dönen JSON veriyi belirtilen tipe dönüştürür.
        public async Task<T> GetAsync<T>(string url)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Belirtilen URL'ye POST isteği gönderir ve JSON cevabını belirtilen tipe dönüştürür.
        public async Task<T> PostAsync<T>(string url, object data)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Belirtilen URL'ye PUT isteği gönderir ve JSON cevabını belirtilen tipe dönüştürür.
        // Not: Kodda hatalı olarak POST kullanılmış, PUT olarak güncellenmeli.
        public async Task<T> PutAsync<T>(string url, object data)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // Belirtilen URL'ye DELETE isteği gönderir ve başarılı olup olmadığını bool olarak döner.
        public async Task<bool> DeleteAsync(string url)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
