using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Interfaces
{
    /// <summary>
    /// Rezervasyon işlemleri, durum güncelleme, rezervasyon iptali
    /// Bağımlı olduğu tablolar : Rezervasyon, RezervasyonPaket
    /// </summary>
    public interface IRezervasyonService
    {  
        Task<RezervasyonDTO> GetByIdAsync(int id);
        Task<IEnumerable<RezervasyonDTO>> GetAllAsync();
        Task<IEnumerable<RezervasyonDTO>> FindAsync(Expression<Func<Rezervasyon, bool>> predicate);
        Task<RezervasyonDTO> CreateAsync(RezervasyonDTO dto);
        Task<RezervasyonDTO> UpdateAsync(RezervasyonDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<RezervasyonDTO> UpdateStatusAsync(int rezervasyonId, RezervasyonDurum yeniDurum);
        Task<bool> CancelReservationAsync(int rezervasyonId);
        Task<bool> AddPackageAsync(int rezervasyonId, int paketId);
        Task<bool> RemovePackageAsync(int rezervasyonId, int paketId);
        Task<IEnumerable<RezervasyonPaketDTO>> GetPackagesAsync(int rezervasyonId);

    }
}
