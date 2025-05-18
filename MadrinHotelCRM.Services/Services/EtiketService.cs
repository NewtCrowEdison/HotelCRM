using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Repositories.Repositories.Concrete;

namespace MadrinHotelCRM.Services.Services
{
    public class EtiketService : IEtiketService
    {
        private readonly IGenericRepository<Etiket> _etiketRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EtiketService(IGenericRepository<Etiket> etiketRepo, IMapper mapper, IUnitOfWork unitofWork)
        {
            _etiketRepo = etiketRepo;
            _mapper = mapper;
            _unitOfWork = unitofWork;
        }
        public async Task<EtiketDTO> CreateAsync(EtiketDTO dto)
        {
            var entity = _mapper.Map<Etiket>(dto);
            await _etiketRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<EtiketDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _etiketRepo.GetByIdAsync(id);
            if (entity == null) return false;
            _etiketRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<EtiketDTO>> GetAllAsync()
        {
            var list = await _etiketRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<EtiketDTO>>(list);
        }

        public async Task<EtiketDTO> GetByIdAsync(int id)
        {
            var entity = await _etiketRepo.GetByIdAsync(id);
            return _mapper.Map<EtiketDTO>(entity);
        }

        public async Task<IEnumerable<EtiketDTO>> FindAsync(Expression<Func<Etiket, bool>> predicate)
        {
            var list = await _etiketRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<EtiketDTO>>(list);
        }

        public async Task<EtiketDTO> UpdateAsync(EtiketDTO dto)
        {
            var entity = _mapper.Map<Etiket>(dto);
            _etiketRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }
    }
}
