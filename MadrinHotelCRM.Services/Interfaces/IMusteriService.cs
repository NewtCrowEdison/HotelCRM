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
        // Temel CRUD operasyonları
        Task<MusteriDTO> GetByIdAsync(int id);
        Task<IEnumerable<MusteriDTO>> GetAllAsync();
        Task<IEnumerable<MusteriDTO>> FindAsync(Expression<Func<Musteri, bool>> predicate);
        Task<MusteriDTO> CreateAsync(MusteriDTO dto);
        Task<MusteriDTO> UpdateAsync(MusteriDTO dto);
        Task<bool> DeleteAsync(int id);


        // Müşteri–Etiket ilişkisi:

        //  müşteriye etiket ekle
        Task<bool> AssignTagAsync(int musteriId, int etiketId);
        //  müşteriden etiketi kaldır
        Task<bool> RemoveTagAsync(int musteriId, int etiketId);

        // Müşteri etkileşim ve geri bildirimleri:

        // Belirli bir müşterinin etkileşimlerini listele
        Task<IEnumerable<MusteriEtkilesimDTO>> GetInteractionsAsync(int musteriId);
        // Belirli bir müşterinin geri bildirimlerini listeler
        Task<IEnumerable<GeriBildirimDTO>> GetFeedbacksAsync(int musteriId);
    }
}
