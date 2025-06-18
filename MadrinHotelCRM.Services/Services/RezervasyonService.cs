using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return entity == null
                ? null
                : _mapper.Map<RezervasyonDTO>(entity);
        }

        public async Task<IEnumerable<RezervasyonDTO>> FindAsync(Expression<Func<Rezervasyon, bool>> predicate)
        {
            var list = await _uow.Read<Rezervasyon>().FindAsync(predicate);
            return _mapper.Map<IEnumerable<RezervasyonDTO>>(list);
        }

        public async Task<RezervasyonDTO> CreateAsync(RezervasyonDTO dto)
        {
            // 1) DTO → Entity, oluşturma tarihi ve durum ataması
            var entity = _mapper.Map<Rezervasyon>(dto);
            entity.OlusturmaTarihi = DateTime.UtcNow;
            entity.Durum = RezervasyonDurum.Onaylandı;

            // 2) Rezervasyonu kaydet
            await _uow.Create<Rezervasyon>().AddAsync(entity);
            await _uow.CommitAsync();

            // 3) Odanın durumunu başlangıç/bitişe göre ayarla
            var oda = await _uow.Read<Oda>().GetByIdAsync(entity.OdaId);
            if (oda != null)
            {
                var now = DateTime.UtcNow;
                var start = entity.BaslangicTarihi;
                var end = entity.BitisTarihi;

                if (now < start)
                    oda.Durum = OdaDurum.Rezervasyonlu;
                else if (now >= start && now <= end)
                    oda.Durum = OdaDurum.Dolu;
                else
                    oda.Durum = OdaDurum.Bos;

                _uow.Update<Oda>().Update(oda);
                await _uow.CommitAsync();
            }

            return _mapper.Map<RezervasyonDTO>(entity);
        }

        public async Task<RezervasyonDTO> UpdateAsync(RezervasyonDTO dto)
        {
            var mevcut = await _uow.Read<Rezervasyon>().GetByIdAsync(dto.RezervasyonId);
            if (mevcut == null) return null;

            // 1) Oda değiştiyse: eski odayı boş, yeni odayı başlangıç/bitişe göre ayarla
            if (mevcut.OdaId != dto.OdaId)
            {
                var eskiOda = await _uow.Read<Oda>().GetByIdAsync(mevcut.OdaId);
                if (eskiOda != null)
                {
                    eskiOda.Durum = OdaDurum.Bos;
                    _uow.Update<Oda>().Update(eskiOda);
                }

                var yeniOda = await _uow.Read<Oda>().GetByIdAsync(dto.OdaId);
                if (yeniOda != null)
                {
                    var now = DateTime.UtcNow;
                    var start = dto.BaslangicTarihi;
                    var end = dto.BitisTarihi;

                    if (now < start)
                        yeniOda.Durum = OdaDurum.Rezervasyonlu;
                    else if (now >= start && now <= end)
                        yeniOda.Durum = OdaDurum.Dolu;
                    else
                        yeniOda.Durum = OdaDurum.Bos;

                    _uow.Update<Oda>().Update(yeniOda);
                }
            }

            // 2) DTO’daki tüm alanları mevcut entity’ye map et ve commit
            _mapper.Map(dto, mevcut);
            _uow.Update<Rezervasyon>().Update(mevcut);
            await _uow.CommitAsync();

            return _mapper.Map<RezervasyonDTO>(mevcut);
        }

        public async Task<IEnumerable<RezervasyonDTO>> GetByOdaIdAsync(int odaId)
        {
            var all = await _uow.Read<Rezervasyon>().FindAsync(r => r.OdaId == odaId);
            return _mapper.Map<IEnumerable<RezervasyonDTO>>(all);
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


        public async Task<bool> CancelReservationAsync(int rezervasyonId, string iptalNedeni)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon == null) return false;

            // 1) Rezervasyon durumu ve iptal nedeni
            rezervasyon.Durum = RezervasyonDurum.İptalEdildi;
            rezervasyon.IptalTarihi = DateTime.UtcNow;
            rezervasyon.IptalNedeni = iptalNedeni;
            _uow.Update<Rezervasyon>().Update(rezervasyon);

            // 2) Odayı tekrar boş duruma geçir
            var oda = await _uow.Read<Oda>().GetByIdAsync(rezervasyon.OdaId);
            if (oda != null)
            {
                oda.Durum = OdaDurum.Bos;
                _uow.Update<Oda>().Update(oda);
            }

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
                rezervasyon.RezervasyonPaketler.Add(new RezervasyonPaket
                {
                    RezervasyonId = rezervasyonId,
                    PaketId = paketId
                });
                _uow.Update<Rezervasyon>().Update(rezervasyon);
                await _uow.CommitAsync();
            }

            return true;
        }

        public async Task<bool> RemovePackageAsync(int rezervasyonId, int paketId)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            var paket = rezervasyon?.RezervasyonPaketler?.FirstOrDefault(rp => rp.PaketId == paketId);
            if (paket == null) return false;

            rezervasyon.RezervasyonPaketler.Remove(paket);
            _uow.Update<Rezervasyon>().Update(rezervasyon);
            await _uow.CommitAsync();

            return true;
        }

        public async Task<IEnumerable<RezervasyonPaketDTO>> GetPackagesAsync(int rezervasyonId)
        {
            var rezervasyon = await _uow.Read<Rezervasyon>().GetByIdAsync(rezervasyonId);
            if (rezervasyon?.RezervasyonPaketler == null)
                return Enumerable.Empty<RezervasyonPaketDTO>();

            return rezervasyon.RezervasyonPaketler
                              .Select(rp => _mapper.Map<RezervasyonPaketDTO>(rp))
                              .ToList();
        }
    }
}
