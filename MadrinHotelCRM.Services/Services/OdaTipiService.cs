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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OdaTipiService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OdaTipiDTO> CreateAsync(OdaTipiDTO dto)
        {
            var entity = _mapper.Map<OdaTipi>(dto);
            await _uow.Create<OdaTipi>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<OdaTipiDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<OdaTipi>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<OdaTipi>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdaTipiDTO>> FindAsync(Expression<Func<OdaTipi, bool>> predicate)
        {
            var list = await _uow.Read<OdaTipi>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<OdaTipiDTO>>(list);
        }

        public async Task<IEnumerable<OdaTipiDTO>> GetAllAsync()
        {
            var list = await _uow.Read<OdaTipi>().GetAllAsync();
            return _mapper.Map<IEnumerable<OdaTipiDTO>>(list);
        }

        public async Task<OdaTipiDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<OdaTipi>().GetByIdAsync(id);
            return _mapper.Map<OdaTipiDTO>(entity);
        }

        public async Task<OdaTipiDTO> UpdateAsync(OdaTipiDTO dto)
        {
            var mevcut = await _uow.Read<OdaTipi>().GetByIdAsync(dto.OdaTipiId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<OdaTipi>().Update(mevcut);
            await _uow.CommitAsync();

            return _mapper.Map<OdaTipiDTO>(mevcut);
        }
    }
}
