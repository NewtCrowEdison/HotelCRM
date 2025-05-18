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
    /// Fatura oluşturma, toplam tutar hesaplama
    /// Bağımlı olduğu tablolar : Fatura
    /// </summary>
    public interface IFaturaService
    {
        Task<FaturaDTO> GetByIdAsync(int id);
        Task<IEnumerable<FaturaDTO>> GetAllAsync();
        Task<IEnumerable<FaturaDTO>> FindAsync(Expression<Func<Fatura, bool>> predicate);
        Task<FaturaDTO> CreateAsync(FaturaDTO dto);
        Task<FaturaDTO> UpdateAsync(FaturaDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
