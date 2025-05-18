using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace MadrinHotelCRM.Services.Services
{
    public class TarifeService : ITarifeService
    {

        private readonly IGenericRepository<Tarife> _tarifeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TarifeService(IGenericRepository<Tarife> tarifeRepo, IMapper mapper, IUnitOfWork unitofWork)
        {
            _tarifeRepo = tarifeRepo;
            _mapper = mapper;
            _unitOfWork = unitofWork;
        }
        public async Task<TarifeDTO> ApplyDiscountAsync(int tarifeId, decimal discountRate)
        {
            var tarife = await _tarifeRepo.GetByIdAsync(tarifeId);
            if (tarife == null)
                return null;
               
           _tarifeRepo.Update(tarife);
           await _unitOfWork.CommitAsync();

            return _mapper.Map<TarifeDTO>(tarife);
        }

        public async Task<TarifeDTO> CreateAsync(TarifeDTO dto)
        {
            var entity = _mapper.Map<Tarife>(dto);
            await _tarifeRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<TarifeDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _tarifeRepo.GetByIdAsync(id);
            if (entity == null) return (false);
            _tarifeRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return (true);
        }

        public async Task<IEnumerable<TarifeDTO>> FindAsync(Expression<Func<Tarife, bool>> predicate)
        {
            var list = await _tarifeRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<TarifeDTO>>(list);
        }

        public async Task<IEnumerable<TarifeDTO>> GetAllAsync()
        {
                var list = await _tarifeRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TarifeDTO>>(list);
        }
            
        public async Task<TarifeDTO> GetByIdAsync(int id)
        {
            var list = await _tarifeRepo.GetByIdAsync(id);
            return _mapper.Map<TarifeDTO>(list);
        }

        public async Task<IEnumerable<TarifeDTO>> GetDiscountedTariffsAsync()
        {
             var tarifeler = await _tarifeRepo.GetAllAsync();

             var indirimliTarifeler = tarifeler
               .Where(t =>  t.IndirimOrani > 0) 
              .Select(t => _mapper.Map<TarifeDTO>(t))
             .ToList();

            return indirimliTarifeler;
        }

        public async  Task<IEnumerable<TarifeDTO>> GetRoomTariffsAsync(int odaId)
        {
                 var tarifeler = await _tarifeRepo.GetAllAsync();
                return tarifeler
                .Where(t => t.OdaTarifeleri.Any(ot => ot.OdaId == odaId) )
                .Select(t => _mapper.Map<TarifeDTO>(t))
                .ToList();

        }


        public async Task<TarifeDTO> UpdateAsync(TarifeDTO dto)
        {
            var entity =  _mapper.Map<Tarife>(dto);
            _tarifeRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }
    }
}
