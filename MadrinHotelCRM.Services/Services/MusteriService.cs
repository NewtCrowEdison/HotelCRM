using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class MusteriService : IMusteriService
    {
        private readonly IUnitOfWork _uow;   
        private readonly IMapper _mapper;     
   
        public MusteriService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;           
            _mapper = mapper;      
        }

        // Yeni  müşteri oluşturma
        public async Task<MusteriDTO> CreateAsync(MusteriDTO dto)
        {
            // DTOyu Entitye dönüştür
            var entity = _mapper.Map<Musteri>(dto);
            //Repositorye ekle 
            await _uow.Create<Musteri>().AddAsync(entity);
            //Değişiklikleri veritabanına kaydet
            await _uow.CommitAsync();
            //Kaydedilen entityi DTOya dönüştürerek döndürme
            return _mapper.Map<MusteriDTO>(entity);
        }

      
        // Belirtilen ID'ye sahip müşteriyi siler.
        public async Task<bool> DeleteAsync(int id)
        {
            // mevcut kaydı alırız
            var entity = await _uow.Read<Musteri>().GetByIdAsync(id);
            if (entity == null)
                return false; // Kayıt yoksa false döndürürüz

            //Silme işlemi için işaretleriz
            _uow.Delete<Musteri>().Delete(entity);
            //Değişiklikleri kaydetme
            await _uow.CommitAsync();
            return true; 
        }

      
        //Koşula uyan müşterileri döner.
        public async Task<IEnumerable<MusteriDTO>> FindAsync(Expression<Func<Musteri, bool>> predicate)
        {
            //Filtreye göre entityleri alırız
            var list = await _uow.Read<Musteri>().FindAsync(predicate);
            //Entity listesini DTO listesine dönüştürürüz
            return _mapper.Map<IEnumerable<MusteriDTO>>(list);
        }

        
        // Tüm müşterileri listeler.
        public async Task<IEnumerable<MusteriDTO>> GetAllAsync()
        {
            //Tüm entityleri çekeriz
            var list = await _uow.Read<Musteri>().GetAllAsync();
            //DTO listesine dönüştürürüz
            return _mapper.Map<IEnumerable<MusteriDTO>>(list);
        }

   
        // Verilen IDye ait müşteriyi döner.
        public async Task<MusteriDTO> GetByIdAsync(int id)
        {
            // Entityyi çekeriz
            var entity = await _uow.Read<Musteri>().GetByIdAsync(id);
            //DTOya dönüştürerek döndürürüz
            return _mapper.Map<MusteriDTO>(entity);
        }

       
        // Mevcut bir müşterinin bilgilerini günceller.
        public async Task<MusteriDTO> UpdateAsync(MusteriDTO dto)
        {
            //Mevcut kaydı veritabanından alırız
            var mevcut = await _uow.Read<Musteri>().GetByIdAsync(dto.MusteriId);
            if (mevcut == null)
                return null; // Kayıt yoksa null döndürürürz

            // DTOdan gelen güncellemeleri mevcut entity üzerine uygularız
            _mapper.Map(dto, mevcut);
            // Güncelleme işlemi için işaretleriz
            _uow.Update<Musteri>().Update(mevcut);
            // Veritabanına değişiklikleri kaydederiz
            await _uow.CommitAsync();
            //Güncellenen entityi DTOya dönüştürerek döndürürüz
            return _mapper.Map<MusteriDTO>(mevcut);
        }

        
        // Müşteriye bir etiket atar.
        public async Task<bool> AssignTagAsync(int musteriId, int etiketId)
        {
            // İlişki tablosu nesnesi oluşturma işlemi :
            var link = new MusteriEtiket { MusteriID = musteriId, EtiketID = etiketId };
            //Ekleme ve kaydetme işlemi:
            await _uow.Create<MusteriEtiket>().AddAsync(link);
            await _uow.CommitAsync();
            return true;
        }

        
        // Müşteriden bir etiketi kaldırır.
        public async Task<bool> RemoveTagAsync(int musteriId, int etiketId)
        {
            //İlgili kayıtları bulur
            var links = await _uow.Read<MusteriEtiket>()
                                 .FindAsync(x => x.MusteriID == musteriId && x.EtiketID == etiketId);
            var link = links.FirstOrDefault();
            if (link == null)
                return false; // Kayıt yoksa false döner

            //Siler ve kaydeder
            _uow.Delete<MusteriEtiket>().Delete(link);
            await _uow.CommitAsync();
            return true;
        }

     
        // Belirli bir müşterinin etkileşim kayıtlarını döner.
        public async Task<IEnumerable<MusteriEtkilesimDTO>> GetInteractionsAsync(int musteriId)
        {
            //Filtreli entityleri çeker
            var list = await _uow.Read<MusteriEtkilesim>().FindAsync(x => x.MusteriID == musteriId);
            //DTO listesine dönüştürür ve geri öner
            return _mapper.Map<IEnumerable<MusteriEtkilesimDTO>>(list);
        }

        
        //Belirli bir müşterinin geri bildirim kayıtlarını döner.
        public async Task<IEnumerable<GeriBildirimDTO>> GetFeedbacksAsync(int musteriId)
        {
            // Filtreli entityleri çekeriz
            var list = await _uow.Read<GeriBildirim>().FindAsync(x => x.MusteriId == musteriId);
            //DTO listesine dönüştürüp döneriz
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(list);
        }
    }
}
