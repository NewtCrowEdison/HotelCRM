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
         private readonly IGenericRepository<Personel> _personelRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PersonelService(IGenericRepository<Personel> personelRepo, IMapper mapper, IUnitOfWork unitofWork)
        {
            _personelRepo = personelRepo;
            _mapper = mapper;
            _unitOfWork = unitofWork;
        }
        public async Task<PersonelDTO> CreateAsync(PersonelDTO dto)
        {
           var entity =  _mapper.Map<Personel>(dto);
            await _personelRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<PersonelDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
             var entity = await _personelRepo.GetByIdAsync(id);
            if (entity == null) return (false);
            _personelRepo.Delete(entity);
            await _unitOfWork.CommitAsync();
            return (true);
        }

        public async Task<IEnumerable<PersonelDTO>> FindAsync(Expression<Func<Personel, bool>> predicate)
        {
             var list = await _personelRepo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<PersonelDTO>>(list);
        }

        public async Task<IEnumerable<PersonelDTO>> GetAllAsync()
        {
            var list = await _personelRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonelDTO>>(list);
        }

        public async Task<PersonelDTO> GetByIdAsync(int id)
        {
             var list = await _personelRepo.GetByIdAsync(id);
            return _mapper.Map<PersonelDTO>(list);
        }

        public async Task<PersonelDTO> UpdateAsync(PersonelDTO dto)
        {
             var entity =  _mapper.Map<Personel>(dto);
            _personelRepo.Update(entity);
            await _unitOfWork.CommitAsync();
            return dto;
        }

        

    }
}
