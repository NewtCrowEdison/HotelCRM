using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class EtkilesimService : IEtkilesimService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public EtkilesimService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<MusteriEtkilesimDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<MusteriEtkilesim>().GetByIdAsync(id);
            return _mapper.Map<MusteriEtkilesimDTO>(entity);
        }

        public async Task<IEnumerable<MusteriEtkilesimDTO>> GetAllAsync()
        {
            var list = await _uow.Read<MusteriEtkilesim>().GetAllAsync();
            return _mapper.Map<IEnumerable<MusteriEtkilesimDTO>>(list);
        }

        public async Task<IEnumerable<MusteriEtkilesimDTO>> FindAsync(Expression<Func<MusteriEtkilesim, bool>> predicate)
        {
            var list = await _uow.Read<MusteriEtkilesim>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<MusteriEtkilesimDTO>>(list);
        }

        public async Task<MusteriEtkilesimDTO> CreateAsync(MusteriEtkilesimDTO dto)
        {
            var entity = _mapper.Map<MusteriEtkilesim>(dto);
            await _uow.Create<MusteriEtkilesim>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<MusteriEtkilesimDTO>(entity);
        }

        public async Task<MusteriEtkilesimDTO> UpdateAsync(MusteriEtkilesimDTO dto)
        {
            var mevcut = await _uow.Read<MusteriEtkilesim>().GetByIdAsync(dto.MusteriEtkilesimId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<MusteriEtkilesim>().Update(mevcut);
            await _uow.CommitAsync();

            return _mapper.Map<MusteriEtkilesimDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<MusteriEtkilesim>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<MusteriEtkilesim>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
