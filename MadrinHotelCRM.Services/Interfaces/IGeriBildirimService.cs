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
    /// Geri bildirim listeleme ve yanıtlama
    /// Bağımlı olduğu tablolar : GeriBildirim
    /// </summary>
    public interface IGeriBildirimService
    {
        Task<GeriBildirimDTO> GetByIdAsync(int id);
        Task<IEnumerable<GeriBildirimDTO>> GetAllAsync();
        Task<IEnumerable<GeriBildirimDTO>> FindAsync(Expression<Func<GeriBildirim, bool>> predicate);
        Task<GeriBildirimDTO> CreateAsync(GeriBildirimDTO dto);
        Task<GeriBildirimDTO> UpdateAsync(GeriBildirimDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
