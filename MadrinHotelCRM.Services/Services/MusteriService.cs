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
        // Repository tanımlamaları yapılıyor:

        // Müşteri verilerini yöneten repository
        private readonly IGenericRepository<Musteri> _musteriRepo;
        // Müşteri-etiket ilişkilerini yöneten repository
        private readonly IGenericRepository<MusteriEtiket> _musteriEtiketRepo;
        // Müşteri geribildirimlerini yöneten repository
        private readonly IGenericRepository<GeriBildirim> _geriBildirimRepo;
        // Müşteri etkileşim kayıtlarını yöneten repository
        private readonly IGenericRepository<MusteriEtkilesim> _interactionRepo;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public MusteriService(IGenericRepository<Musteri> musteriRepo, IGenericRepository<MusteriEtiket> musteriEtiketRepo, IGenericRepository<GeriBildirim> geriBildirimRepo, IGenericRepository<MusteriEtkilesim> interactionRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _musteriRepo = musteriRepo;
            _musteriEtiketRepo = musteriEtiketRepo;
            _geriBildirimRepo = geriBildirimRepo;
            _interactionRepo = interactionRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        // Yeni bir müşteri oluşturur.
        public async Task<MusteriDTO> CreateAsync(MusteriDTO dto)
        {
            var entity = _mapper.Map<Musteri>(dto);//Gelen DTO'yu entity'e çevirmek için 
            await _musteriRepo.AddAsync(entity);   //Repository'e eklemek için
            await _unitOfWork.CommitAsync();       //UnitOfWork ile değişiklikleri kaydetme işlemi
            return _mapper.Map<MusteriDTO>(entity); // Oluşturulan entity'i tekrar DTO'ya map'leyip döner.
        }
        // Belirtilen id'ye sahip müşteriyi siler.
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _musteriRepo.GetByIdAsync(id);
            if (entity == null)
                return false;

            _musteriRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }


        // Koşula uyan müşterileri bularak onları DTO listesi olarak dönme;
        public async Task<IEnumerable<MusteriDTO>> FindAsync(Expression<Func<Musteri, bool>> predicate)
        {
            var entities = await _musteriRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<MusteriDTO>>(entities);
        }

        // Tüm müşterileri liste olarak alırız:
        public async Task<IEnumerable<MusteriDTO>> GetAllAsync()
        {
            var entities = await _musteriRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<MusteriDTO>>(entities);
        }

        // Id bazlı müşteri getirmek için;
        public async Task<MusteriDTO> GetByIdAsync(int id)
        {
            var entity = await _musteriRepo.GetByIdAsync(id);
            return _mapper.Map<MusteriDTO>(entity);
        }

        // Mevcut bir müşteriyi günceller.
        public async Task<MusteriDTO> UpdateAsync(MusteriDTO dto)
        {
            var entity = _mapper.Map<Musteri>(dto); // DTO'dan entity'ye dönüştürme işlemi.
            _musteriRepo.Update(entity);  //Repository üzerinden güncelleme işlemi.
            await _unitOfWork.CommitAsync();  // Değişiklikleri kaydeder ve güncel DTO'yu döner.
            return _mapper.Map<MusteriDTO>(entity);
        }

        // Müşteriye yeni bir etiket atamak için yapılır:
        public async Task<bool> AssignTagAsync(int musteriId, int etiketId)
        {
            var link = new MusteriEtiket { MusteriID = musteriId, EtiketID = etiketId };
            await _musteriEtiketRepo.AddAsync(link);
            await _unitOfWork.CommitAsync();
            return true;
        }
        // Müşteriden belirtilen etiketi kaldırmak için:
        public async Task<bool> RemoveTagAsync(int musteriId, int etiketId)
        {
            var links = await _musteriEtiketRepo.FindAsync(x => x.MusteriID == musteriId && x.EtiketID == etiketId);

            //Bulunanlar arasından ilkini al(aynı kayıttan birden fazlası olmamalı)
            var link = links.FirstOrDefault();

            if (link == null)
                return false; // Kaldırılacak etiket yoksa false döner

            _musteriEtiketRepo.Delete(link);
            await _unitOfWork.CommitAsync();
            return true;
        }

        // Belirtilen müşterinin geribildirimlerini döndürmek için:
        public async Task<IEnumerable<GeriBildirimDTO>> GetFeedbacksAsync(int musteriId)
        {
            var geriBildirimler = await _geriBildirimRepo.FindAsync(x => x.MusteriId == musteriId);
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(geriBildirimler);
        }

        // Belirtilen müşterinin etkileşim kayıtlarını döndürmek için: 
        public async Task<IEnumerable<MusteriEtkilesimDTO>> GetInteractionsAsync(int musteriId)
        {
            var etkilesimler = await _interactionRepo.FindAsync(x => x.MusteriID == musteriId);
            return _mapper.Map<IEnumerable<MusteriEtkilesimDTO>>(etkilesimler); // DTO listesinde çevirip döndürürüz
        }
    }
}
