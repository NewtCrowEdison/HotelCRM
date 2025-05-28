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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TarifeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TarifeDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Tarife>().GetByIdAsync(id);
            return _mapper.Map<TarifeDTO>(entity);
        }

        public async Task<IEnumerable<TarifeDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Tarife>().GetAllAsync();
            return _mapper.Map<IEnumerable<TarifeDTO>>(list);
        }

        public async Task<IEnumerable<TarifeDTO>> FindAsync(Expression<Func<Tarife, bool>> predicate)
        {
            var list = await _uow.Read<Tarife>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<TarifeDTO>>(list);
        }

        public async Task<TarifeDTO> CreateAsync(TarifeDTO dto)
        {
            var entity = _mapper.Map<Tarife>(dto);
            await _uow.Create<Tarife>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<TarifeDTO>(entity);
        }

        public async Task<TarifeDTO> UpdateAsync(TarifeDTO dto)
        {
            var mevcut = await _uow.Read<Tarife>().GetByIdAsync(dto.TarifeId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Tarife>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<TarifeDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Tarife>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Tarife>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<TarifeDTO> ApplyDiscountAsync(int tarifeId, decimal discountRate)
        {
            var entity = await _uow.Read<Tarife>().GetByIdAsync(tarifeId);
            if (entity == null) return null;

            // İndirimi uygula
            entity.IndirimOrani = (int)discountRate;
            _uow.Update<Tarife>().Update(entity);
            await _uow.CommitAsync();

            return _mapper.Map<TarifeDTO>(entity);
        }

        public async Task<IEnumerable<TarifeDTO>> GetDiscountedTariffsAsync()
        {
            // İndirim oranı > 0 olan tarifeleri getir
            var list = await _uow.Read<Tarife>().FindAsync(t => t.IndirimOrani > 0);
            return _mapper.Map<IEnumerable<TarifeDTO>>(list);
        }

        public async Task<IEnumerable<TarifeDTO>> GetRoomTariffsAsync(int odaId)
        {
            // Odaya ait tarifeleri getir
            var list = await _uow.Read<Tarife>().FindAsync(t => t.OdaTarifeleri.Any(ot => ot.OdaId == odaId));
            return _mapper.Map<IEnumerable<TarifeDTO>>(list);
        }
    }
}
