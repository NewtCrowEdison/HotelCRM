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
    /// Sistem logları filtreleme, listeleme
    /// Bağımlı olduğu tablolar : SistemLog
    /// </summary>
    public interface ILogService
    {
        Task<SistemLogDTO> GetByIdAsync(int id);
        Task<IEnumerable<SistemLogDTO>> GetAllAsync();
        Task<IEnumerable<SistemLogDTO>> FindAsync(Expression<Func<SistemLog, bool>> predicate);
        Task<SistemLogDTO> CreateAsync(SistemLogDTO dto);
        Task<SistemLogDTO> UpdateAsync(SistemLogDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
