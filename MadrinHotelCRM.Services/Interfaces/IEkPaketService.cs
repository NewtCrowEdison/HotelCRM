using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;

namespace MadrinHotelCRM.Services.Interfaces
{
    public interface IEkPaketService
    {
        Task<IEnumerable<EkPaketDTO>> GetAllAsync();
        Task<EkPaketDTO> GetByIdAsync(int id);
        Task<EkPaketDTO> CreateAsync(EkPaketDTO dto);
        Task<EkPaketDTO> UpdateAsync(EkPaketDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
