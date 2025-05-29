using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;

namespace MadrinHotelCRM.Services.Services
{
    public class EkPaketService : IEkPaketService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EkPaketService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<EkPaketDTO> CreateAsync(EkPaketDTO dto)
        {
            var entity = _mapper.Map<EkPaket>(dto);
            await _uow.Create<EkPaket>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<EkPaketDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<EkPaket>().GetByIdAsync(id);
            if (entity == null) return false;
            _uow.Delete<EkPaket>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<EkPaketDTO>> GetAllAsync()
        {
            var list = await _uow.Read<EkPaket>().GetAllAsync();
            return _mapper.Map<IEnumerable<EkPaketDTO>>(list);
        }

        public async Task<EkPaketDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<EkPaket>().GetByIdAsync(id);
            return _mapper.Map<EkPaketDTO>(entity);
        }

        public async Task<EkPaketDTO> UpdateAsync(EkPaketDTO dto)
        {
            var mevcut = await _uow.Read<EkPaket>().GetByIdAsync(dto.EkPaketId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<EkPaket>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<EkPaketDTO>(mevcut);
        }
    }
}
