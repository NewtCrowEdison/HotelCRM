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
    public class RezervasyonService : IRezervasyonService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RezervasyonService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RezervasyonDTO>> GetAllAsync()
        {
            var list = await _uow.Read<Rezervasyon>().GetAllAsync();
            return _mapper.Map<IEnumerable<RezervasyonDTO>>(list);
        }

        public async Task<RezervasyonDTO> GetByIdAsync(int id)
        {
            var entity = await _uow.Read<Rezervasyon>().GetByIdAsync(id);
            return _mapper.Map<RezervasyonDTO>(entity);
        }

        public async Task<IEnumerable<RezervasyonDTO>> FindAsync(Expression<Func<Rezervasyon, bool>> predicate)
        {
            var list = await _uow.Read<Rezervasyon>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<RezervasyonDTO>>(list);
        }

        public async Task<RezervasyonDTO> CreateAsync(RezervasyonDTO dto)
        {
            var entity = _mapper.Map<Rezervasyon>(dto);
            await _uow.Create<Rezervasyon>().AddAsync(entity);
            await _uow.CommitAsync();
            return _mapper.Map<RezervasyonDTO>(entity);
        }

        public async Task<RezervasyonDTO> UpdateAsync(RezervasyonDTO dto)
        {
            var mevcut = await _uow.Read<Rezervasyon>().GetByIdAsync(dto.RezervasyonId);
            if (mevcut == null) return null;
            _mapper.Map(dto, mevcut);
            _uow.Update<Rezervasyon>().Update(mevcut);
            await _uow.CommitAsync();
            return _mapper.Map<RezervasyonDTO>(mevcut);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Rezervasyon>().GetByIdAsync(id);
            if (entity == null) return false;
            _uow.Delete<Rezervasyon>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<RezervasyonDTO> UpdateStatusAsync(int rezervasyonId, RezervasyonDurum yeniDurum)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon == null) return null;
            rezervasyon.Durum = yeniDurum;
            _uow.Update<Rezervasyon>().Update(rezervasyon);
            await _uow.CommitAsync();
            return _mapper.Map<RezervasyonDTO>(rezervasyon);
        }

        public async Task<bool> CancelReservationAsync(int rezervasyonId)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon == null) return false;
            rezervasyon.Durum = RezervasyonDurum.İptalEdildi;
            _uow.Update<Rezervasyon>().Update(rezervasyon);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<bool> AddPackageAsync(int rezervasyonId, int paketId)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon == null) return false;
            rezervasyon.RezervasyonPaketler ??= new List<RezervasyonPaket>();
            if (!rezervasyon.RezervasyonPaketler.Any(rp => rp.PaketId == paketId))
            {
                rezervasyon.RezervasyonPaketler.Add(new RezervasyonPaket { RezervasyonId = rezervasyonId, PaketId = paketId });
                _uow.Update<Rezervasyon>().Update(rezervasyon);
                await _uow.CommitAsync();
            }
            return true;
        }

        public async Task<bool> RemovePackageAsync(int rezervasyonId, int paketId)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon?.RezervasyonPaketler == null) return false;
            var paket = rezervasyon.RezervasyonPaketler.FirstOrDefault(rp => rp.PaketId == paketId);
            if (paket == null) return false;
            rezervasyon.RezervasyonPaketler.Remove(paket);
            _uow.Update<Rezervasyon>().Update(rezervasyon);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<RezervasyonPaketDTO>> GetPackagesAsync(int rezervasyonId)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon?.RezervasyonPaketler == null) return Enumerable.Empty<RezervasyonPaketDTO>();
            return rezervasyon.RezervasyonPaketler.Select(rp => _mapper.Map<RezervasyonPaketDTO>(rp)).ToList();
        }
    }
}
