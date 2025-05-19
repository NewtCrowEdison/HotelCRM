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
    public class GeriBildirimService : IGeriBildirimService
    {
        private readonly IGenericRepository<GeriBildirim> _geriBildirimRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GeriBildirimService(IGenericRepository<GeriBildirim> geriBildirimRepo,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _geriBildirimRepo = geriBildirimRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GeriBildirimDTO> CreateAsync(GeriBildirimDTO dto)
        {
            var entity = _mapper.Map<GeriBildirim>(dto);
            await _geriBildirimRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GeriBildirimDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _geriBildirimRepo.GetByIdAsync(id);
            if (entity == null) return false;

            _geriBildirimRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<GeriBildirimDTO>> FindAsync(Expression<Func<GeriBildirim, bool>> predicate)
        {
            var entities = await _geriBildirimRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(entities);
        }

        public async Task<IEnumerable<GeriBildirimDTO>> GetAllAsync()
        {
            var entities = await _geriBildirimRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<GeriBildirimDTO>>(entities);
        }

        public async Task<GeriBildirimDTO> GetByIdAsync(int id)
        {
            var entity = await _geriBildirimRepo.GetByIdAsync(id);
            return _mapper.Map<GeriBildirimDTO>(entity);
        }

        public async Task<GeriBildirimDTO> UpdateAsync(GeriBildirimDTO dto)
        {
            var entity = _mapper.Map<GeriBildirim>(dto);
            _geriBildirimRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<GeriBildirimDTO>(entity);
        }
    }
}
