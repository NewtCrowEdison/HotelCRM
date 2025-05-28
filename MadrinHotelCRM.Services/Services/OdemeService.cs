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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OdemeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OdemeDTO> CreateAsync(OdemeDTO dto)
        {
            var entity = _mapper.Map<Odeme>(dto);
            await _uow.Create<Odeme>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<OdemeDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Odeme>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Odeme>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdemeDTO>> FindAsync(Expression<Func<Odeme, bool>> predicate)
        {
            var list = await _uow.Read<Odeme>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<OdemeDTO>>(list);
        }

        public async Task<IEnumerable<OdemeDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Odeme>().GetAllAsync();
            return _mapper.Map<IEnumerable<OdemeDTO>>(list);
        }

        public async Task<OdemeDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Odeme>().GetByIdAsync(id);
            return _mapper.Map<OdemeDTO>(entity);
        }

        public async Task<OdemeDTO> UpdateAsync(OdemeDTO dto)
        {
            var mevcut = await _uow.Read<Odeme>().GetByIdAsync(dto.OdemeId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Odeme>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<OdemeDTO>(mevcut);
        }
    }
}

