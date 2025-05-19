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
    public class OdaTipiService : IOdaTipiService
    {
        private readonly IGenericRepository<OdaTipi> _odaTipiRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OdaTipiService(
            IGenericRepository<OdaTipi> odaTipiRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _odaTipiRepo = odaTipiRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OdaTipiDTO> CreateAsync(OdaTipiDTO dto)
        {
            var entity = _mapper.Map<OdaTipi>(dto);
            await _odaTipiRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<OdaTipiDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _odaTipiRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _odaTipiRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdaTipiDTO>> FindAsync(Expression<Func<OdaTipi, bool>> predicate)
        {
            var entities = await _odaTipiRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<OdaTipiDTO>>(entities);
        }

        public async Task<IEnumerable<OdaTipiDTO>> GetAllAsync()
        {
            var entities = await _odaTipiRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OdaTipiDTO>>(entities);
        }

        public async Task<OdaTipiDTO> GetByIdAsync(int id)
        {
            var entity = await _odaTipiRepo.GetByIdAsync(id);
            return _mapper.Map<OdaTipiDTO>(entity);
        }

        public async Task<OdaTipiDTO> UpdateAsync(OdaTipiDTO dto)
        {
            var entity = _mapper.Map<OdaTipi>(dto);
            _odaTipiRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }
    }
}
