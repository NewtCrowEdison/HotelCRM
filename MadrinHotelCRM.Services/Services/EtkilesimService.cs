using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class EtkilesimService : IEtkilesimService
    {
        private readonly IGenericRepository<MusteriEtkilesim> _etkilesimRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EtkilesimService(
            IGenericRepository<MusteriEtkilesim> etkilesimRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _etkilesimRepo = etkilesimRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MusteriEtkilesimDTO> GetByIdAsync(int id)
        {
            var entity = await _etkilesimRepo.GetByIdAsync(id);
            return _mapper.Map<MusteriEtkilesimDTO>(entity);
        }

        public async Task<IEnumerable<MusteriEtkilesimDTO>> GetAllAsync()
        {
            var entities = await _etkilesimRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<MusteriEtkilesimDTO>>(entities);
        }

        public async Task<IEnumerable<MusteriEtkilesimDTO>> FindAsync(Expression<Func<MusteriEtkilesim, bool>> predicate)
        {
            var entities = await _etkilesimRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<MusteriEtkilesimDTO>>(entities);
        }

        public async Task<MusteriEtkilesimDTO> CreateAsync(MusteriEtkilesimDTO dto)
        {
            var entity = _mapper.Map<MusteriEtkilesim>(dto);
            await _etkilesimRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<MusteriEtkilesimDTO>(entity);
        }

        public async Task<MusteriEtkilesimDTO> UpdateAsync(MusteriEtkilesimDTO dto)
        {
            var entity = _mapper.Map<MusteriEtkilesim>(dto);
            _etkilesimRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _etkilesimRepo.GetByIdAsync(id);
            if (entity == null)
                return false;

            _etkilesimRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
