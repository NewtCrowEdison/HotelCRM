using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Repositories.Repositories.Interfaces
{
    public interface IGenericRepository<T> :
        IReadRepository<T>,
        ICreateRepository<T>,
        IUpdateRepository<T>,
        IDeleteRepository<T>
        where T : class
    {
    }
}
