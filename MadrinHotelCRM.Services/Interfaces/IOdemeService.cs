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
    /// Ödeme işlemleri
    /// Bağımlı olduğu tablolar : Odeme
    /// </summary>
    public interface IOdemeService
    {
        Task<OdemeDTO> GetByIdAsync(int id);
        Task<IEnumerable<OdemeDTO>> GetAllAsync();
        Task<IEnumerable<OdemeDTO>> FindAsync(Expression<Func<Odeme, bool>> predicate);
        Task<OdemeDTO> CreateAsync(OdemeDTO dto);
        Task<OdemeDTO> UpdateAsync(OdemeDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
