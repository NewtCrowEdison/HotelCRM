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
    public class DepartmanService : IDepartmanService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DepartmanService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmanDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Departman>().GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmanDTO>>(list);
        }

        public async Task<DepartmanDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Departman>().GetByIdAsync(id);
            return _mapper.Map<DepartmanDTO>(entity);
        }

        public async Task<DepartmanDTO> CreateAsync(DepartmanDTO dto)
        {
            var entity = _mapper.Map<Departman>(dto);
            await _uow.Create<Departman>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<DepartmanDTO>(entity);
        }

        public async Task<DepartmanDTO> UpdateAsync(DepartmanDTO dto)
        {
            var mevcut = await _uow.Read<Departman>().GetByIdAsync(dto.DepartmanId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Departman>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<DepartmanDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Departman>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Departman>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
