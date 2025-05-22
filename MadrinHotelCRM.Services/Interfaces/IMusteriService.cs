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
        Task<MusteriDTO> GetByIdAsync(int id); // Id ile eşleşen müşteriyi getirir
        Task<IEnumerable<MusteriDTO>> GetAllAsync(); // bütün müşterileri listeler 
        Task<IEnumerable<MusteriDTO>> FindAsync(Expression<Func<Musteri, bool>> predicate); // koşula uyan müşteriyi gettirir
        Task<MusteriDTO> CreateAsync(MusteriDTO dto); // yeni müşteri oluşturmak için
        Task<MusteriDTO> UpdateAsync(MusteriDTO dto); // Müşteriyi güncellemek için
        Task<bool> DeleteAsync(int id); // seçilen müşteriyi silmek için 


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
