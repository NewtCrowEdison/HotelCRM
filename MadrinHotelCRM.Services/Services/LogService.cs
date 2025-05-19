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
        private readonly IGenericRepository<SistemLog> _logRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LogService(IGenericRepository<SistemLog> logRepo,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _logRepo = logRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SistemLogDTO> CreateAsync(SistemLogDTO dto)
        {
            var entity = _mapper.Map<SistemLog>(dto);
            await _logRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SistemLogDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _logRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _logRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<SistemLogDTO>> FindAsync(Expression<Func<SistemLog, bool>> predicate)
        {
            var entities = await _logRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<SistemLogDTO>>(entities);
        }

        public async Task<IEnumerable<SistemLogDTO>> GetAllAsync()
        {
            var entities = await _logRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<SistemLogDTO>>(entities);
        }

        public async Task<SistemLogDTO> GetByIdAsync(int id)
        {
            var entity = await _logRepo.GetByIdAsync(id);
            return _mapper.Map<SistemLogDTO>(entity);
        }

        public async Task<SistemLogDTO> UpdateAsync(SistemLogDTO dto)
        {
            var entity = _mapper.Map<SistemLog>(dto);
            _logRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<SistemLogDTO>(entity);
        }
    }
}