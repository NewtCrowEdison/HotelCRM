using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class MusteriRezervasyonService : IMusteriRezervasyonService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MusteriRezervasyonService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MusteriRezervasyonDTO>> GetAllAsync()
        {
            var list = await _uow.Read<MusteriRezervasyon>().GetAllAsync();
            return _mapper.Map<IEnumerable<MusteriRezervasyonDTO>>(list);
        }

        public async Task<MusteriRezervasyonDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<MusteriRezervasyon>().GetByIdAsync(id);
            return _mapper.Map<MusteriRezervasyonDTO>(entity);
        }

        public async Task<MusteriRezervasyonDTO> CreateAsync(MusteriRezervasyonDTO dto)
        {
            var entity = _mapper.Map<MusteriRezervasyon>(dto);
            await _uow.Create<MusteriRezervasyon>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<MusteriRezervasyonDTO>(entity);
        }

        public async Task<MusteriRezervasyonDTO> UpdateAsync(MusteriRezervasyonDTO dto)
        {
            var mevcut = await _uow.Read<MusteriRezervasyon>().GetByIdAsync(dto.RezervasyonId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<MusteriRezervasyon>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<MusteriRezervasyonDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<MusteriRezervasyon>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<MusteriRezervasyon>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
