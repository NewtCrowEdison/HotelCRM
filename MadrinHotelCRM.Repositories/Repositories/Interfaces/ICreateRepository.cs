using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Repositories.Repositories.Interfaces
{
    public interface ICreateRepository<T> where T : class
    {
        Task AddAsync(T entity);
    }
}
