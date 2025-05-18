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
    public class OdaService : IOdaService
    {
        private readonly IGenericRepository<Oda> _odaRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OdaService(
           IGenericRepository<Oda> odaRepo,
           IMapper mapper,
           IUnitOfWork unitOfWork)
        {
            _odaRepo = odaRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<OdaDTO> CreateAsync(OdaDTO dto)
        {
            var entity = _mapper.Map<Oda>(dto);
            await _odaRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<OdaDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _odaRepo.GetByIdAsync(id);
            if (entity == null) return false;
            _odaRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdaDTO>> FindAsync(Expression<Func<Oda, bool>> predicate)
        {
            var list = await _odaRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<OdaDTO>>(list);
        }

        public async Task<IEnumerable<OdaDTO>> GetAllAsync()
        {
            var list = await _odaRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OdaDTO>>(list);
        }

        public async Task<OdaDTO> GetByIdAsync(int id)
        {
            var entity = await _odaRepo.GetByIdAsync(id);
            return _mapper.Map<OdaDTO>(entity);
        }

        public async Task<OdaDTO> UpdateAsync(OdaDTO dto)
        {
            var entity = _mapper.Map<Oda>(dto);
            _odaRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }

        public async Task<bool> UpdateRoomStatusAsync(int odaId, string yeniDurum)
        {
            var oda = await _odaRepo.GetByIdAsync(odaId);
            if (oda == null) return false;

            if (Enum.TryParse(yeniDurum, out OdaDurum durum))
            {
                oda.Durum = durum;
                _odaRepo.Update(oda);
                await _unitOfWork.CommitAsync();
                return true;
            }

            return false; // Eğer enum dönüştürülemezse false döneriz
        }


        public async Task<bool> UpdateTariffAsync(int odaId, int tarifeId)
        {
            var oda = await _odaRepo.GetByIdAsync(odaId);
            if (oda == null) return false;

            oda.OdaTipiId = tarifeId;
            _odaRepo.Update(oda);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
