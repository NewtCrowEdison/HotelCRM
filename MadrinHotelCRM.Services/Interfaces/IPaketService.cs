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
    /// Ek paketleri yönetme
    /// Bağımlı olduğu tablolar : EkPaket
    /// </summary>
    public interface IPaketService
    {
        Task<EkPaketDTO> GetByIdAsync(int id);
        Task<IEnumerable<EkPaketDTO>> GetAllAsync();
        Task<IEnumerable<EkPaketDTO>> FindAsync(Expression<Func<EkPaket, bool>> predicate);
        Task<EkPaketDTO> CreateAsync(EkPaketDTO dto);
        Task<EkPaketDTO> UpdateAsync(EkPaketDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
