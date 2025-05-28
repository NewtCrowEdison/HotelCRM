using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Interfaces
{
    public interface IMusteriRezervasyonService
    {
        Task<IEnumerable<MusteriRezervasyonDTO>> GetAllAsync();
        Task<MusteriRezervasyonDTO> GetByIdAsync(int id);
        Task<MusteriRezervasyonDTO> CreateAsync(MusteriRezervasyonDTO dto);
        Task<MusteriRezervasyonDTO> UpdateAsync(MusteriRezervasyonDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
