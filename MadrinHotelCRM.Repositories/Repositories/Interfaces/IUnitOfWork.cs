using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Repositories.Repositories.Interfaces
{
    /// <summary>
    /// IUnitOfWork arayüzü, tüm veritabanı işlemleri için repository'leri tek bir merkezden yönetmeyi sağlar.
    /// Bu yapı, veritabanı işlemlerinde "Transaction" benzeri davranış sağlar.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IReadRepository<T> Read<T>() where T : class;
        ICreateRepository<T> Create<T>() where T : class;
        IUpdateRepository<T> Update<T>() where T : class;
        IDeleteRepository<T> Delete<T>() where T : class;
        Task<int> CommitAsync();
    }
}

