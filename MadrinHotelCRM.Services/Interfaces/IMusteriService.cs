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
    /// Müşteri CRUD, etiketleme, etkileşimler, geri bildirimler
    /// Bağımlı olduğu tablolar : Musteri, MusteriEtkilesim, MusteriEtiket, GeriBildirim
    /// </summary>
    public interface IMusteriService
    {
        Task<MusteriDTO> GetByIdAsync(int id);
        Task<IEnumerable<MusteriDTO>> GetAllAsync();
        Task<IEnumerable<MusteriDTO>> FindAsync(Expression<Func<Musteri, bool>> predicate);
        Task<MusteriDTO> CreateAsync(MusteriDTO dto);
        Task<MusteriDTO> UpdateAsync(MusteriDTO dto);
        Task<bool> DeleteAsync(int id);

        Task<bool> AssignTagAsync(int musteriId, int etiketId);
        Task<bool> RemoveTagAsync(int musteriId, int etiketId);

        Task<IEnumerable<MusteriEtkilesimDTO>> GetInteractionsAsync(int musteriId);
        Task<IEnumerable<GeriBildirimDTO>> GetFeedbacksAsync(int musteriId);
    }
}
