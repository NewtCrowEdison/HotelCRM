using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Services.Interfaces
{
    /// <summary>
    /// IHttpClientService, HTTP işlemleri için ortak bir arayüz sağlar.
    /// IHttpClientFactory ile çalışacak şekilde tasarlanmıştır ve test edilebilirliği artırır.
    /// </summary>
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);
        Task<T> PostAsync<T>(string url, object data);
        Task<T> PutAsync<T>(string url, object data);
        Task<bool> DeleteAsync(string url);
    }
}
