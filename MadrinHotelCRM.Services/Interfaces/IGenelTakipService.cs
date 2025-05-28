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
    /// Genel işlem geçmişi takibi
    /// Bağımlı olduğu tablolar : GenelTakip
    /// </summary>
    public interface IGenelTakipService
    {
        Task<GenelTakipDTO> GetByIdAsync(int id);
        Task<IEnumerable<GenelTakipDTO>> GetAllAsync();
        Task<IEnumerable<GenelTakipDTO>> FindAsync(Expression<Func<GenelTakip, bool>> predicate);
        Task<GenelTakipDTO> CreateAsync(GenelTakipDTO dto);
        Task<GenelTakipDTO> UpdateAsync(GenelTakipDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
