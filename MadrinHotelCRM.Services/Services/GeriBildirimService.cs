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
        private readonly IGenericRepository<GeriBildirim> _geriBildirimRepo;
        private readonly IMapper _mapper; // Dto - Entity eşlemelerini gerçekleştirmek için
        private readonly IUnitOfWork _unitOfWork; // Değişiklikleri toplu kaydetmekiçin

        public GeriBildirimService(IGenericRepository<GeriBildirim> geriBildirimRepo,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _geriBildirimRepo = geriBildirimRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // geri bildirim oluşturmak için 
        public async Task<GeriBildirimDTO> CreateAsync(GeriBildirimDTO dto)
        {
            var entity = _mapper.Map<GeriBildirim>(dto); // Dto yu entity e çeviririz
            await _geriBildirimRepo.AddAsync(entity); // Yeni kaydı ekleriz
            await _unitOfWork.CommitAsync(); // veritabanına kaydederiz
            return _mapper.Map<GeriBildirimDTO>(entity); // oluşturulan entitiy i tekrar dto ya döndürürüz
        }

        // Silme işlemi için :
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _geriBildirimRepo.GetByIdAsync(id); // eşleşen ID deki kaydı çekeriz
            if (entity == null)
            {
                return false;
            } // o Id de kayıt yok ise false döner 
            _geriBildirimRepo.Delete(entity); // eşleşme var ise sileriz
            await _unitOfWork.CommitAsync(); // veritabanının son halini kaydederiz
            return true;
        }

        // koşula uyan geribiidirm kayıtlarını listelemek için 
        public async Task<IEnumerable<GeriBildirimDTO>> FindAsync(Expression<Func<GeriBildirim, bool>> filtre)
        {
            var entities = await _geriBildirimRepo.FindAsync(filtre); // koşula uyan kayıtları buluruz
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(entities); // DTO listesi olarak döneriz
        }
        
        // geri bildirim kayıtlarını lstelemek için 
        public async Task<IEnumerable<GeriBildirimDTO>> GetAllAsync()
        {
            var entities = await _geriBildirimRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(entities);
        }

        // id ile eşleşen kayıtları listelemek için
        public async Task<GeriBildirimDTO> GetByIdAsync(int id)
        {
            var entity = await _geriBildirimRepo.GetByIdAsync(id);
            return _mapper.Map<GeriBildirimDTO>(entity);
        }

        // güncelleme yapabilmek iiçin ;
        public async Task<GeriBildirimDTO> UpdateAsync(GeriBildirimDTO dto)
        { 
            var entity = _mapper.Map<GeriBildirim>(dto); // DTO yu Entity e çevirmek için
            _geriBildirimRepo.Update(entity); // Repoda güncelleme işlemini gerçekleştirirz
            await _unitOfWork.CommitAsync(); // veritabanına kaydederiz
            return _mapper.Map<GeriBildirimDTO>(entity); // entity i tekrar dto ya çevirir döneriz
        }
    }
}
