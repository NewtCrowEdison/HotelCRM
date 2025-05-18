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
    public class GenelTakipService : IGenelTakipService
    {
        private readonly IGenericRepository<GenelTakip> _genelTakipRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GenelTakipService(
            IGenericRepository<GenelTakip> genelTakipRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _genelTakipRepo = genelTakipRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenelTakipDTO> GetByIdAsync(int id)
        {
            var entity = await _genelTakipRepo.GetByIdAsync(id);
            return _mapper.Map<GenelTakipDTO>(entity);
        }

        public async Task<IEnumerable<GenelTakipDTO>> GetAllAsync()
        {
            var entities = await _genelTakipRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<GenelTakipDTO>>(entities);
        }

        public async Task<IEnumerable<GenelTakipDTO>> FindAsync(Expression<Func<GenelTakip, bool>> predicate)
        {
            var entities = await _genelTakipRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<GenelTakipDTO>>(entities);
        }

        public async Task<GenelTakipDTO> CreateAsync(GenelTakipDTO dto)
        {
            var entity = _mapper.Map<GenelTakip>(dto);
            await _genelTakipRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GenelTakipDTO>(entity);
        }

        public async Task<GenelTakipDTO> UpdateAsync(GenelTakipDTO dto)
        {
            var entity = _mapper.Map<GenelTakip>(dto);
            _genelTakipRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _genelTakipRepo.GetByIdAsync(id);
            if (entity == null)
                return false;

            _genelTakipRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
