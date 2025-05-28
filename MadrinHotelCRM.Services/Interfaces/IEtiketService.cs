using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Interfaces
{
    /// <summary>
    /// Etiket oluşturma, silme ve müşteri etiketleme
    /// Bağımlı olduğu tablolar: Etiket, MusteriEtiket
    /// </summary>
    public interface IEtiketService
    {
        Task<EtiketDTO> CreateAsync(EtiketDTO dto);
        Task<IEnumerable<EtiketDTO>> GetAllAsync();
        Task<EtiketDTO> GetByIdAsync(int id);
        Task<IEnumerable<EtiketDTO>> FindAsync(Expression<Func<Etiket, bool>> predicate);
        Task<EtiketDTO> UpdateAsync(EtiketDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
