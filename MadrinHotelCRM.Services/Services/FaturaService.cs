using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly IGenericRepository<Fatura> _faturaRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FaturaService(
            IGenericRepository<Fatura> faturaRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _faturaRepo = faturaRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FaturaDTO> GetByIdAsync(int id)
        {
            var entity = await _faturaRepo.GetByIdAsync(id);
            return _mapper.Map<FaturaDTO>(entity);
        }

        public async Task<IEnumerable<FaturaDTO>> GetAllAsync()
        {
            var entities = await _faturaRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<FaturaDTO>>(entities);
        }

        public async Task<IEnumerable<FaturaDTO>> FindAsync(Expression<Func<Fatura, bool>> predicate)
        {
            var entities = await _faturaRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<FaturaDTO>>(entities);
        }

        public async Task<FaturaDTO> CreateAsync(FaturaDTO dto)
        {
            var entity = _mapper.Map<Fatura>(dto);
            await _faturaRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<FaturaDTO>(entity);
        }

        public async Task<FaturaDTO> UpdateAsync(FaturaDTO dto)
        {
            var entity = _mapper.Map<Fatura>(dto);
            _faturaRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _faturaRepo.GetByIdAsync(id);
            if (entity == null)
                return false;

            _faturaRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
