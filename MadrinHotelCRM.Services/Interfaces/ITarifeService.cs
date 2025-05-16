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
    /// Tarife yönetimi, indirim kontrolleri
    /// Bağımlı olduğu tablolar : Tarife, OdaTarife
    /// </summary>
    public interface ITarifeService
    {
        Task<TarifeDTO> GetByIdAsync(int id);
        Task<IEnumerable<TarifeDTO>> GetAllAsync();
        Task<IEnumerable<TarifeDTO>> FindAsync(Expression<Func<Tarife, bool>> predicate);
        Task<TarifeDTO> CreateAsync(TarifeDTO dto);
        Task<TarifeDTO> UpdateAsync(TarifeDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<TarifeDTO> ApplyDiscountAsync(int tarifeId, decimal discountRate);
        Task<IEnumerable<TarifeDTO>> GetDiscountedTariffsAsync();
        Task<IEnumerable<TarifeDTO>> GetRoomTariffsAsync(int odaId);
    }

}

