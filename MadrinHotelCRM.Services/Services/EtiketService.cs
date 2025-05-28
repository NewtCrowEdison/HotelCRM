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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EtiketService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<EtiketDTO> CreateAsync(EtiketDTO dto)
        {
            var entity = _mapper.Map<Etiket>(dto);
            await _uow.Create<Etiket>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<EtiketDTO>(entity);
        }

        public async Task<IEnumerable<EtiketDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Etiket>().GetAllAsync();
            return _mapper.Map<IEnumerable<EtiketDTO>>(list);
        }

        public async Task<EtiketDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Etiket>().GetByIdAsync(id);
            return _mapper.Map<EtiketDTO>(entity);
        }

        public async Task<IEnumerable<EtiketDTO>> FindAsync(Expression<Func<Etiket, bool>> predicate)
        {
            var list = await _uow.Read<Etiket>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<EtiketDTO>>(list);
        }

        public async Task<EtiketDTO> UpdateAsync(EtiketDTO dto)
        {
            var mevcut = await _uow.Read<Etiket>().GetByIdAsync(dto.EtiketId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Etiket>().Update(mevcut);
            await _uow.CommitAsync();

            return _mapper.Map<EtiketDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Etiket>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Etiket>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
