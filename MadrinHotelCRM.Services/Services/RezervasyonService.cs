using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class RezervasyonService : IRezervasyonService
    {

         private readonly IGenericRepository<Rezervasyon> _rezervasyonRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RezervasyonService(IGenericRepository<Rezervasyon> rezervasyonRepo, IMapper mapper, IUnitOfWork unitofWork)
        {
            _rezervasyonRepo = rezervasyonRepo;
            _mapper = mapper;
            _unitOfWork = unitofWork;
        }
        public async Task<bool> AddPackageAsync(int rezervasyonId, int paketId)
        {
            var rezervasyon = await _rezervasyonRepo.GetByIdAsync(rezervasyonId);
             if (rezervasyon == null)
             return false;

             rezervasyon.RezervasyonPaketler.Any(rp=> rp.PaketId == paketId);

             _rezervasyonRepo.Update(rezervasyon);
             await _unitOfWork.CommitAsync();

             return true;
        }

        public async Task<bool> CancelReservationAsync(int rezervasyonId)
        {
            var rezervasyon = await _rezervasyonRepo.GetByIdAsync(rezervasyonId);
            if (rezervasyon == null)
                  return false;

             rezervasyon.Durum = RezervasyonDurum.İptalEdildi;


             _rezervasyonRepo.Update(rezervasyon);
             await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<RezervasyonDTO> CreateAsync(RezervasyonDTO dto)
        {
             var entity = _mapper.Map<Rezervasyon>(dto);
            await _rezervasyonRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<RezervasyonDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _rezervasyonRepo.GetByIdAsync(id);
            if (entity == null) return (false);
            _rezervasyonRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return (true);
        }

        public async Task<IEnumerable<RezervasyonDTO>> FindAsync(Expression<Func<Rezervasyon, bool>> predicate)
        {
             var list = await _rezervasyonRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<RezervasyonDTO>>(list);
        }

        public async Task<IEnumerable<RezervasyonDTO>> GetAllAsync()
        {
            var list = await _rezervasyonRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<RezervasyonDTO>>(list);
        }

        public async Task<RezervasyonDTO> GetByIdAsync(int id)
        {
            var list = await _rezervasyonRepo.GetByIdAsync(id);
            return _mapper.Map<RezervasyonDTO>(list);
        }

        public async Task<IEnumerable<RezervasyonPaketDTO>> GetPackagesAsync(int rezervasyonId)
        {
             var rezervasyon = await _rezervasyonRepo.GetByIdAsync(rezervasyonId);
             if (rezervasyon == null)
                  return Enumerable.Empty<RezervasyonPaketDTO>();

             var paketler = rezervasyon.RezervasyonPaketler ?? new List<RezervasyonPaket>();

             var paketDtos = paketler.Select(p => _mapper.Map<RezervasyonPaketDTO>(p)).ToList();

             return paketDtos;
        }

        public async Task<bool> RemovePackageAsync(int rezervasyonId, int paketId)
        {
             var rezervasyon = await _rezervasyonRepo.GetByIdAsync(rezervasyonId);
             if (rezervasyon == null)
                   return false;

             var paket = rezervasyon.RezervasyonPaketler?.FirstOrDefault(rp => rp.PaketId == paketId);
               if (paket == null)
                 return false;

            rezervasyon.RezervasyonPaketler.Remove(paket);

             _rezervasyonRepo.Update(rezervasyon);
              await _unitOfWork.CommitAsync();

              return true;
        }

        public async Task<RezervasyonDTO> UpdateAsync(RezervasyonDTO dto)
        {
             var entity =  _mapper.Map<Rezervasyon>(dto);
            _rezervasyonRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }

        public async Task<RezervasyonDTO> UpdateStatusAsync(int rezervasyonId, RezervasyonDurum yeniDurum)
        {
              var rezervasyon = await _rezervasyonRepo.GetByIdAsync(rezervasyonId);
              if (rezervasyon == null)
                      return null;

             rezervasyon.Durum = yeniDurum;

             _rezervasyonRepo.Update(rezervasyon);
             await _unitOfWork.CommitAsync();

             return _mapper.Map<RezervasyonDTO>(rezervasyon);
        }
    }
}
