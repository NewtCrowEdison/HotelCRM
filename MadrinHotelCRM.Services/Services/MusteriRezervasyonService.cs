using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class MusteriRezervasyonService : IMusteriRezervasyonService
    {
        private readonly IGenericRepository<MusteriRezervasyon> _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MusteriRezervasyonService(
            IGenericRepository<MusteriRezervasyon> repo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MusteriRezervasyonDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<MusteriRezervasyonDTO>>(entities);
        }

        public async Task<MusteriRezervasyonDTO> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<MusteriRezervasyonDTO>(entity);
        }

        public async Task<MusteriRezervasyonDTO> CreateAsync(MusteriRezervasyonDTO dto)
        {
            var entity = _mapper.Map<MusteriRezervasyon>(dto);
            await _repo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<MusteriRezervasyonDTO>(entity);
        }

        public async Task<MusteriRezervasyonDTO> UpdateAsync(MusteriRezervasyonDTO dto)
        {
            var entity = _mapper.Map<MusteriRezervasyon>(dto);
            _repo.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<MusteriRezervasyonDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
