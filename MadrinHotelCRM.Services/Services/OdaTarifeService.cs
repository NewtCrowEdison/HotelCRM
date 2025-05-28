using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MadrinHotelCRM.Services.Services
{
    public class OdaTarifeService : IOdaTarifeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OdaTarifeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OdaTarifeDTO>> GetAllAsync()
        {
            var list = await _uow.Read<OdaTarife>().GetAllAsync();
            return _mapper.Map<IEnumerable<OdaTarifeDTO>>(list);
        }

        public async Task<OdaTarifeDTO> AddAsync(OdaTarifeDTO dto)
        {
            var entity = _mapper.Map<OdaTarife>(dto);
            await _uow.Create<OdaTarife>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<OdaTarifeDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int odaId, int tarifeId)
        {
            var existing = (await _uow.Read<OdaTarife>()
                .FindAsync(x => x.OdaId == odaId && x.TarifeId == tarifeId))
                .FirstOrDefault();

            if (existing == null)
                return false;

            _uow.Delete<OdaTarife>().Delete(existing);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdaTarifeDTO>> GetByOdaIdAsync(int odaId)
        {
            var list = await _uow.Read<OdaTarife>().FindAsync(x => x.OdaId == odaId);
            return _mapper.Map<IEnumerable<OdaTarifeDTO>>(list);
        }

        public async Task<IEnumerable<OdaTarifeDTO>> GetByTarifeIdAsync(int tarifeId)
        {
            var list = await _uow.Read<OdaTarife>().FindAsync(x => x.TarifeId == tarifeId);
            return _mapper.Map<IEnumerable<OdaTarifeDTO>>(list);
        }

        public async Task<OdaTarifeDTO> GetDetailsAsync(int odaId, int tarifeId)
        {
            var existing = (await _uow.Read<OdaTarife>()
                .FindAsync(x => x.OdaId == odaId && x.TarifeId == tarifeId))
                .FirstOrDefault();

            if (existing == null)
                return null;

            return _mapper.Map<OdaTarifeDTO>(existing);
        }


    }
}