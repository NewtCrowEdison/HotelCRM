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
    public class PaketService : IPaketService
    {
        private readonly IGenericRepository<EkPaket> _paketRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PaketService(
            IGenericRepository<EkPaket> paketRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _paketRepo = paketRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EkPaketDTO> CreateAsync(EkPaketDTO dto)
        {
            var entity = _mapper.Map<EkPaket>(dto);
            await _paketRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<EkPaketDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _paketRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _paketRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<EkPaketDTO>> FindAsync(Expression<Func<EkPaket, bool>> predicate)
        {
            var entities = await _paketRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<EkPaketDTO>>(entities);
        }

        public async Task<IEnumerable<EkPaketDTO>> GetAllAsync()
        {
            var entities = await _paketRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<EkPaketDTO>>(entities);
        }

        public async Task<EkPaketDTO> GetByIdAsync(int id)
        {
            var entity = await _paketRepo.GetByIdAsync(id);
            return _mapper.Map<EkPaketDTO>(entity);
        }

        public async Task<EkPaketDTO> UpdateAsync(EkPaketDTO dto)
        {
            var entity = _mapper.Map<EkPaket>(dto);
            _paketRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }
    }
}
