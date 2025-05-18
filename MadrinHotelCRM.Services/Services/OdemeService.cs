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
    public class OdemeService : IOdemeService
    {
        private readonly IGenericRepository<Odeme> _odemeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OdemeService(
            IGenericRepository<Odeme> odemeRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _odemeRepo = odemeRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OdemeDTO> CreateAsync(OdemeDTO dto)
        {
            var entity = _mapper.Map<Odeme>(dto);
            await _odemeRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<OdemeDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _odemeRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _odemeRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdemeDTO>> FindAsync(Expression<Func<Odeme, bool>> predicate)
        {
            var entities = await _odemeRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<OdemeDTO>>(entities);
        }

        public async Task<IEnumerable<OdemeDTO>> GetAllAsync()
        {
            var entities = await _odemeRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OdemeDTO>>(entities);
        }

        public async Task<OdemeDTO> GetByIdAsync(int id)
        {
            var entity = await _odemeRepo.GetByIdAsync(id);
            return _mapper.Map<OdemeDTO>(entity);
        }

        public async Task<OdemeDTO> UpdateAsync(OdemeDTO dto)
        {
            var entity = _mapper.Map<Odeme>(dto);
            _odemeRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }
    }
}

