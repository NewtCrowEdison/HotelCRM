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
        Task<GeriBildirimDTO> GetByIdAsync(int id); // eşleşen Id deki geri bildirimi getirir
        Task<IEnumerable<GeriBildirimDTO>> GetAllAsync(); // bütün geri bildirimleri listeler
        Task<IEnumerable<GeriBildirimDTO>> FindAsync(Expression<Func<GeriBildirim, bool>> filtre); // filtre koşuluna uyan tüm geri bildirimleri listeler
        Task<GeriBildirimDTO> CreateAsync(GeriBildirimDTO dto); // yeni bir geri bilriim oluşturmayı sağlar
        Task<GeriBildirimDTO> UpdateAsync(GeriBildirimDTO dto); // mevcut bir ger bildirimi güncellemeyi sağlar
        Task<bool> DeleteAsync(int id); // seçilen Id li geri bildirim kaydını silmemizi sağlar
    }
}
