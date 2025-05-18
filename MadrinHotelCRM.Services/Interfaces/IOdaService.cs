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
    /// Oda CRUD, oda durumu güncelleme
    /// Bağımlı olduğu tablolar : Oda, OdaTarife, OdaTipi
    /// </summary>
    public interface IOdaService
    {
        Task<OdaDTO> GetByIdAsync(int id);
        Task<IEnumerable<OdaDTO>> GetAllAsync();
        Task<IEnumerable<OdaDTO>> FindAsync(Expression<Func<Oda, bool>> predicate);
        Task<OdaDTO> CreateAsync(OdaDTO dto);
        Task<OdaDTO> UpdateAsync(OdaDTO dto);
        Task<bool> DeleteAsync(int id);

        // Odanın durumunu (boş/dolu vs.) günceller
        Task<bool> UpdateRoomStatusAsync(int odaId, string yeniDurum);

        // Odaya yeni tarifeyi atar veya günceller
        Task<bool> UpdateTariffAsync(int odaId, int tarifeId);
    }
}
