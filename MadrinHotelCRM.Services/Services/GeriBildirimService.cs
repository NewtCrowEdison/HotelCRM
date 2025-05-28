using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class GeriBildirimService : IGeriBildirimService
    {
        private readonly IUnitOfWork _uow; // repository erişimi ve transaction kontrolu yaparız
        private readonly IMapper _mapper; // Entity - DTO dönüşümleri gerçekleştirilir.

    
        public GeriBildirimService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;        
            _mapper = mapper;      
        }

        // Belirtilen ID'ye sahip geri bildirimi getirir.
        public async Task<GeriBildirimDTO> GetByIdAsync(int id)
        {
            // Repository'den entity'yi alırız
            var entity = await _uow.Read<GeriBildirim>().GetByIdAsync(id);
            // Entity'i DTO'ya dönüştürüp gerii döndürürüz
            return _mapper.Map<GeriBildirimDTO>(entity);
        }

        // Tüm geri bildirim kayıtlarını listelemek için;
        public async Task<IEnumerable<GeriBildirimDTO>> GetAllAsync()
        {
            //Tüm entity'leri çekeriz
            var entities = await _uow.Read<GeriBildirim>().GetAllAsync();
            //Listeyi DTO listesine dönüştürüp geri döndürürüz
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(entities);
        }

  
        // Belirli bir filtreye uyan geri bildirimleri getirmek için
        public async Task<IEnumerable<GeriBildirimDTO>> FindAsync(Expression<Func<GeriBildirim, bool>> predicate)
        {
            //Filtreye uygun entityleri çekeriz
            var entities = await _uow.Read<GeriBildirim>().FindAsync(predicate);
            //DTO listesine dönüştürürüz ve döndürürürz
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(entities);
        }

        // Yeni bir geri bildirim oluşturma:
        public async Task<GeriBildirimDTO> CreateAsync(GeriBildirimDTO dto)
        {
            //DTO'yu Entity'e dönüştürme:
            var entity = _mapper.Map<GeriBildirim>(dto);
            //Repositorye ekleme
            await _uow.Create<GeriBildirim>().AddAsync(entity);
            //Değişiklikleri kaydetme
            await _uow.CommitAsync();
            //Kaydedilen entityi DTOya dönüştürüp döndürme
            return _mapper.Map<GeriBildirimDTO>(entity);
        }

     
        // Mevcut bir geri bildirimi günceller.
        public async Task<GeriBildirimDTO> UpdateAsync(GeriBildirimDTO dto)
        {
            //Mevcut kaydı çekeriz
            var mevcut = await _uow.Read<GeriBildirim>().GetByIdAsync(dto.GeriBildirimId);
            if (mevcut == null)
                return null;    // Kayıt yoksa null döneriz

            //DTO'dan gelen alanlarla mevcut entityi güncelleriz
            _mapper.Map(dto, mevcut);
            //Değişiklik işaretleme yapılır
            _uow.Update<GeriBildirim>().Update(mevcut);
            //Kaydet
            await _uow.CommitAsync();
            //Güncellenen entity'i DTO'ya dönüştürür ve döndürürüz
            return _mapper.Map<GeriBildirimDTO>(mevcut);
        }

        // Belirtilen ID'ye sahip geri bildirimi siler.
        public async Task<bool> DeleteAsync(int id)
        {
            //Kaydı çekeriz
            var entity = await _uow.Read<GeriBildirim>().GetByIdAsync(id);
            if (entity == null)
                return false;   // Kayıt yoksa false döneriz

            //Silme işlemi
            _uow.Delete<GeriBildirim>().Delete(entity);
            //Kaydetme 
            await _uow.CommitAsync();
            return true;      
        }
    }
}
