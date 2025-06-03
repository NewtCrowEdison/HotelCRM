// Services/Services/PersonelService.cs
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
            var repo = _uow.Create<Personel>();
            var entity = _mapper.Map<Personel>(dto);

            await repo.AddAsync(entity);
            await _uow.CommitAsync();

            return _mapper.Map<PersonelDTO>(entity);
        }

        public async Task<PersonelDTO> GetByIdAsync(int id)
        {
            var repo = _uow.Read<Personel>();
            var entity = await repo.GetByIdAsync(id)
                         ?? throw new KeyNotFoundException($"Personel {id} bulunamadı.");
            return _mapper.Map<PersonelDTO>(entity);
        }

        public async Task<IEnumerable<PersonelDTO>> GetAllAsync()
        {
            var repo = _uow.Read<Personel>();
            var list = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonelDTO>>(list);
        }

        public async Task<IEnumerable<PersonelDTO>> FindAsync(Expression<Func<Personel, bool>> predicate)
        {
            var repo = _uow.Read<Personel>();
            var list = await repo.FindAsync(predicate);
            return _mapper.Map<IEnumerable<PersonelDTO>>(list);
        }

        public async Task<PersonelDTO> UpdateAsync(PersonelDTO dto)
        {
            var readRepo = _uow.Read<Personel>();
            var entity = await readRepo.GetByIdAsync(dto.PersonelId)
                            ?? throw new KeyNotFoundException($"Personel {dto.PersonelId} bulunamadı.");

            _mapper.Map(dto, entity);

            var updRepo = _uow.Update<Personel>();
            updRepo.Update(entity);
            await _uow.CommitAsync();

            return _mapper.Map<PersonelDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var readRepo = _uow.Read<Personel>();
            var entity = await readRepo.GetByIdAsync(id);
            if (entity == null) return false;

            var delRepo = _uow.Delete<Personel>();
            delRepo.Delete(entity);
            await _uow.CommitAsync();

            return true;
        }
        //şifre değşiştirme işlemi için
        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO dto)
        {
            var repo = _uow.Read<Personel>();
            var list = await repo.FindAsync(p => p.KullaniciId == dto.KullaniciId);
            var entity = list.FirstOrDefault();

            if (entity == null)
                throw new KeyNotFoundException("Kullanıcı bulunamadı.");

            if (entity.PasswordHash != dto.EskiSifre) //  HASH ile karşılaştırılmalı
                throw new InvalidOperationException("Eski şifre yanlış.");

            if (dto.YeniSifre != dto.YeniSifreTekrar)
                throw new InvalidOperationException("Yeni şifreler eşleşmiyor.");

            entity.PasswordHash = dto.YeniSifre; //  HASH ile kaydedilmeli
            var updRepo = _uow.Update<Personel>();
            updRepo.Update(entity);
            await _uow.CommitAsync();
            return true;
        }
        public async Task<PersonelDTO> GetByKullaniciIdAsync(string kullaniciId)
        {
            var repo = _uow.Read<Personel>();
            var list = await repo.FindAsync(p => p.KullaniciId == kullaniciId);
            var entity = list.FirstOrDefault();

            if (entity == null)
                throw new KeyNotFoundException("Kullanıcı bulunamadı.");

            return _mapper.Map<PersonelDTO>(entity);
        }


    }
}
