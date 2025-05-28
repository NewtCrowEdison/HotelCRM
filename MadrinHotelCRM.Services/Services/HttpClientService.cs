using MadrinHotelCRM.Services.Interfaces;
using System.Net.Http.Json;

namespace MadrinHotelCRM.Services.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
