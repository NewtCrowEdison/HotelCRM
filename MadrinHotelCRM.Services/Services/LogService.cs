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
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _uow;// Veritabanı işlemlerini yönetmek için UnitOfWork entegrasyonu
        private readonly IMapper _mapper;// Entity-DTO dönüşümlerini sağlamak için mapper

      
        public LogService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;            
            _mapper = mapper;     
        }

        // Verilen ID’ye ait log kaydını getirir.
        public async Task<SistemLogDTO> GetByIdAsync(int id)
        {
            //Repositoryden entityyi çekeriz
            var entity = await _uow.Read<SistemLog>().GetByIdAsync(id);
            //Entityi DTOya dönüştürüp döndürürüz
            return _mapper.Map<SistemLogDTO>(entity);
        }

    
        // Tüm log kayıtlarını listeler.
        public async Task<IEnumerable<SistemLogDTO>> GetAllAsync()
        {
            //Tüm entityleri çekeriz
            var list = await _uow.Read<SistemLog>().GetAllAsync();
            //Listeyi DTO listesine dönüştürürüz döndürüz
            return _mapper.Map<IEnumerable<SistemLogDTO>>(list);
        }

       
        // Belirli bir filtreye uyan log kayıtlarını getirir.
        public async Task<IEnumerable<SistemLogDTO>> FindAsync(Expression<Func<SistemLog, bool>> predicate)
        {
            // Filtreye göre entityleri çekeriz
            var list = await _uow.Read<SistemLog>().FindAsync(predicate);
            // DTO listesine dönüştürür döndürürüz
            return _mapper.Map<IEnumerable<SistemLogDTO>>(list);
        }

      
        // Yeni bir log kaydı oluşturur.
        public async Task<SistemLogDTO> CreateAsync(SistemLogDTO dto)
        {
            //DTOdan entityye dönüştürürz
            var entity = _mapper.Map<SistemLog>(dto);
            // Repositorye ekleriz
            await _uow.Create<SistemLog>().AddAsync(entity);
            //Değişiklikleri kaydetme işlemi
            await _uow.CommitAsync();
            //Kaydedilen entityi DTOya dönüştürüp döndürürüz
            return _mapper.Map<SistemLogDTO>(entity);
        }

        // Mevcut bir log kaydını güncelleriz:
        public async Task<SistemLogDTO> UpdateAsync(SistemLogDTO dto)
        {
            //Mevcut kaydı çekeriz
            var mevcut = await _uow.Read<SistemLog>().GetByIdAsync(dto.SistemLogId);
            if (mevcut == null)
                return null;    // Kayıt bulunamazsa null döndürmek için

            //DTOdan gelen alanlarla mevcut entityyi güncelleme
            _mapper.Map(dto, mevcut);
            //Değişiklikleri işaretleme
            _uow.Update<SistemLog>().Update(mevcut);
            //Kaydetme işlemi
            await _uow.CommitAsync();
            //Güncellenen entityi DTOya dönüştürüp  döndürürüz
            return _mapper.Map<SistemLogDTO>(mevcut);
        }

        
        // Belirtilen ID’ye sahip log kaydını siler.
        public async Task<bool> DeleteAsync(int id)
        {
            //Kaydı çekeriz
            var entity = await _uow.Read<SistemLog>().GetByIdAsync(id);
            if (entity == null)
                return false;   // Kayıt yoksa false döndürür

            //Silme işlemini yaparız
            _uow.Delete<SistemLog>().Delete(entity);
            //Veritabanını Kaydederiz
            await _uow.CommitAsync();
            return true;
        }
    }
}