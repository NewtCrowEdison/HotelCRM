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
        private readonly IGenericRepository<SistemLog> _logRepo; // veritabanındaki SistemLog kayıtlarına erişebilmek ve güncelleme yapabilmek için

        private readonly IMapper _mapper; // DTO ve Entitiy arasında dönüşüm yapabilmek için

        private readonly IUnitOfWork _unitOfWork; // Repo üzerinde yapılan birden fazla değişikliği tek bir işlem ile kaydetmeyi sağlayabilmek için

        public LogService(IGenericRepository<SistemLog> logRepo,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _logRepo = logRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        } // dışarıdan gelen logRepo, mapper ve unitOfWork alarak servis içinde kullanabilmeyi sağlamak için Constructor.

        // Kayıt oluşturma anında kullanılır 
        public async Task<SistemLogDTO> CreateAsync(SistemLogDTO dto)
        {
            var entity = _mapper.Map<SistemLog>(dto); // DTO dan Entitye dönüştürürüz 
            await _logRepo.AddAsync(entity); // Repoya ekleme işlemini gerçekleştiririz
            await _unitOfWork.CommitAsync(); // Değişikliği veritabanına kaydetme işlemini gerçekleştiririz
            return _mapper.Map<SistemLogDTO>(entity); // Entityi tekrardan DTO ya çeviririz
        }

        //Silme işelmi için kullanırız: 
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _logRepo.GetByIdAsync(id); // Id ile entity getiririz
            if (entity == null) 
            {
                return false;
            } // eğer Id ile eşleşen yok ise false döneriz

            _logRepo.Delete(entity); // eşleşen var ise sileriz
            await _unitOfWork.CommitAsync(); // değişikliği kaydederiz
            return true; 
        }

        public async Task<IEnumerable<SistemLogDTO>> FindAsync(Expression<Func<SistemLog, bool>> filtre) // bu parametre ile hangi kayıtları isteyeceğimzi belirtiriz
        {
            var logKayitlari = await _logRepo.FindAsync(filtre); // sadece filtreye uyan kayıtları çekmek için
            return _mapper.Map<IEnumerable<SistemLogDTO>>(logKayitlari); // entity modelini dto ya çevirerek döndürmek için
        }

        // Tüm kayıtlatı Listelemek için: 
        public async Task<IEnumerable<SistemLogDTO>> GetAllAsync()
        {
            var entities = await _logRepo.GetAllAsync(); // tüm kayıtları alırız
            return _mapper.Map<IEnumerable<SistemLogDTO>>(entities); // DTO ya çevirip geri döndürürüz
        } 

        // Id ile eşleşen kaydı getimek için kullanırız :
        public async Task<SistemLogDTO> GetByIdAsync(int id)
        {
            var entity = await _logRepo.GetByIdAsync(id); // Id ile eşleşen kaydı alırız
            return _mapper.Map<SistemLogDTO>(entity); // DTO ya çevirerek tekrar yollarız
        }

        // Kayıt güncelleme için kullanırız: 
        public async Task<SistemLogDTO> UpdateAsync(SistemLogDTO dto)
        {
            var entity = _mapper.Map<SistemLog>(dto); // DTO dan Entity e çeviririz
            _logRepo.Update(entity); // Repodaki güncelleme metodunu çağırırarak güncelleriz
            await _unitOfWork.CommitAsync(); // Kayıt işlemini gerçekleştirirz
            return _mapper.Map<SistemLogDTO>(entity); // güncelediğim entitiy i DTOya dönüştürüp döndürürüm.
        }
    }
}