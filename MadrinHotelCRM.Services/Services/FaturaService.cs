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
    public class FaturaService : IFaturaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FaturaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<FaturaDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Fatura>().GetByIdAsync(id);
            return _mapper.Map<FaturaDTO>(entity);
        }

        public async Task<IEnumerable<FaturaDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Fatura>().GetAllAsync();
            return _mapper.Map<IEnumerable<FaturaDTO>>(list);
        }

        public async Task<IEnumerable<FaturaDTO>> FindAsync(Expression<Func<Fatura, bool>> predicate)
        {
            var list = await _uow.Read<Fatura>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<FaturaDTO>>(list);
        }

        public async Task<FaturaDTO> CreateAsync(FaturaDTO dto)
        {
            var entity = _mapper.Map<Fatura>(dto);
            await _uow.Create<Fatura>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<FaturaDTO>(entity);
        }

        public async Task<FaturaDTO> UpdateAsync(FaturaDTO dto)
        {
            var mevcut = await _uow.Read<Fatura>().GetByIdAsync(dto.FaturaId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Fatura>().Update(mevcut);
            await _uow.CommitAsync();

            return _mapper.Map<FaturaDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Fatura>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Fatura>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
