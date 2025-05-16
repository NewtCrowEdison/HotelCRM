using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Interfaces
{
    /// <summary>
    /// Oda tipi CRUD
    /// Bağımlı olduğu tablolar : OdaTipi
    /// </summary>
    public interface IOdaTipiService
    {
        Task<OdaTipiDTO> GetByIdAsync(int id);
        Task<IEnumerable<OdaTipiDTO>> GetAllAsync();
        Task<IEnumerable<OdaTipiDTO>> FindAsync(Expression<Func<OdaTipi, bool>> predicate);
        Task<OdaTipiDTO> CreateAsync(OdaTipiDTO dto);
        Task<OdaTipiDTO> UpdateAsync(OdaTipiDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
