using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    public class PersonelService : IPersonelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PersonelService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PersonelDTO> CreateAsync(PersonelDTO dto)
        {
            var entity = _mapper.Map<Personel>(dto);
            await _uow.Create<Personel>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<PersonelDTO>(entity);
        }

        public async Task<PersonelDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Personel>().GetByIdAsync(id);
            return _mapper.Map<PersonelDTO>(entity);
        }

        public async Task<IEnumerable<PersonelDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Personel>().GetAllAsync();
            return _mapper.Map<IEnumerable<PersonelDTO>>(list);
        }

        public async Task<IEnumerable<PersonelDTO>> FindAsync(Expression<Func<Personel, bool>> predicate)
        {
            var list = await _uow.Read<Personel>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<PersonelDTO>>(list);
        }

        public async Task<PersonelDTO> UpdateAsync(PersonelDTO dto)
        {
            var mevcut = await _uow.Read<Personel>().GetByIdAsync(dto.PersonelId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Personel>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<PersonelDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Personel>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Personel>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }
    }
}
