using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class OdaService : IOdaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OdaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<OdaDTO> CreateAsync(OdaDTO dto)
        {
            var entity = _mapper.Map<Oda>(dto);
            await _uow.Create<Oda>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<OdaDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Oda>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Oda>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdaDTO>> FindAsync(Expression<Func<Oda, bool>> predicate)
        {
            var list = await _uow.Read<Oda>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<OdaDTO>>(list);
        }

        public async Task<IEnumerable<OdaDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Oda>().GetAllAsync();
            return _mapper.Map<IEnumerable<OdaDTO>>(list);
        }

        public async Task<OdaDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Oda>().GetByIdAsync(id);
            return _mapper.Map<OdaDTO>(entity);
        }

        public async Task<OdaDTO> UpdateAsync(OdaDTO dto)
        {
            var mevcut = await _uow.Read<Oda>().GetByIdAsync(dto.OdaId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Oda>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<OdaDTO>(mevcut);
        }

        public async Task<bool> UpdateRoomStatusAsync(int odaId, string yeniDurum)
        {
            var oda = await _uow.Read<Oda>().GetByIdAsync(odaId);
            if (oda == null) return false;

            if (Enum.TryParse(yeniDurum, out OdaDurum durum))
            {
                oda.Durum = durum;
                _uow.Update<Oda>().Update(oda);
                await _uow.CommitAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateTariffAsync(int odaId, int tarifeId)
        {
            var oda = await _uow.Read<Oda>().GetByIdAsync(odaId);
            if (oda == null) return false;

            oda.OdaTipiId = tarifeId;
            _uow.Update<Oda>().Update(oda);
            await _uow.CommitAsync();
            return true;
        }
    }
}
