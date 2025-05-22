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
        Task<SistemLogDTO> GetByIdAsync(int id); // belirtilen id deki logu getirir
        Task<IEnumerable<SistemLogDTO>> GetAllAsync(); // tüm logları getirir
        Task<IEnumerable<SistemLogDTO>> FindAsync(Expression<Func<SistemLog, bool>> filtre); // filtreye uyan logları çekmemizi sağlar
        Task<SistemLogDTO> CreateAsync(SistemLogDTO dto); //yeni log kaydı oluşturmayı sağlar
        Task<SistemLogDTO> UpdateAsync(SistemLogDTO dto); // kayıtlı log da güncelleme yapmayı sağlar
        Task<bool> DeleteAsync(int id); // eşleşen Id deki log kaydını silmemizi sağlar 
    }
}
