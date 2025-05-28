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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GenelTakipService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<GenelTakipDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<GenelTakip>().GetByIdAsync(id);
            return _mapper.Map<GenelTakipDTO>(entity);
        }

        public async Task<IEnumerable<GenelTakipDTO>> GetAllAsync()
        {
            var list = await _uow.Read<GenelTakip>().GetAllAsync();
            return _mapper.Map<IEnumerable<GenelTakipDTO>>(list);
        }

        public async Task<IEnumerable<GenelTakipDTO>> FindAsync(Expression<Func<GenelTakip, bool>> predicate)
        {
            var list = await _uow.Read<GenelTakip>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<GenelTakipDTO>>(list);
        }

        public async Task<GenelTakipDTO> CreateAsync(GenelTakipDTO dto)
        {
            var entity = _mapper.Map<GenelTakip>(dto);
            await _uow.Create<GenelTakip>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<GenelTakipDTO>(entity);
        }

        public async Task<GenelTakipDTO> UpdateAsync(GenelTakipDTO dto)
        {
            var mevcut = await _uow.Read<GenelTakip>().GetByIdAsync(dto.GenelTakipId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<GenelTakip>().Update(mevcut);
            await _uow.CommitAsync();

            return _mapper.Map<GenelTakipDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<GenelTakip>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<GenelTakip>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
