using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class DepartmanService : IDepartmanService
    {
        private readonly IGenericRepository<Departman> _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmanService(
            IGenericRepository<Departman> repo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmanDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmanDTO>>(entities);
        }

        public async Task<DepartmanDTO> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return _mapper.Map<DepartmanDTO>(entity);
        }

        public async Task<DepartmanDTO> CreateAsync(DepartmanDTO dto)
        {
            var entity = _mapper.Map<Departman>(dto);
            await _repo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DepartmanDTO>(entity);
        }

        public async Task<DepartmanDTO> UpdateAsync(DepartmanDTO dto)
        {
            var entity = _mapper.Map<Departman>(dto);
            _repo.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DepartmanDTO>(entity);
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
