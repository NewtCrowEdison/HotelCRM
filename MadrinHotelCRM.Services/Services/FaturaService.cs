using System;
using System.Collections.Generic;
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
        public async Task<FaturaDTO> CreateFromRezervasyonAsync(int rezervasyonId)
        {
            //  Rezervasyon’u çek
            var rez = await _uow.Read<Entities.Models.Rezervasyon>()
                                .GetByIdAsync(rezervasyonId);
            if (rez == null)
                return null!;  // veya throw

            //  İlgili Tarife ve OdaTipi’ni de tek tek çek
            var tarife = await _uow.Read<Entities.Models.Tarife>()
                                   .GetByIdAsync(rez.TarifeId);

            var oda = await _uow.Read<Entities.Models.Oda>()
                                .GetByIdAsync(rez.OdaId);
            var odaTipi = await _uow.Read<Entities.Models.OdaTipi>()
                                    .GetByIdAsync(oda.OdaTipiId);

            // Hesaplama (oda tipi fiyat + tarife fiyat)
            var toplamTutar = odaTipi.Fiyat + tarife.Fiyat;

            // Fatura entity’sini oluştur
            var faturaEntity = new Entities.Models.Fatura
            {
                RezervasyonId = rezervasyonId,
                ToplamTutar = toplamTutar,
                Durum = Entities.Enums.FaturaDurum.Odenmedi,
                FaturaOlusturmaTarihi = DateTime.UtcNow
            };
            await _uow.Create<Entities.Models.Fatura>()
                      .AddAsync(faturaEntity);
            await _uow.CommitAsync();

            //  DTO’ya map et ve döndür
            return _mapper.Map<FaturaDTO>(faturaEntity);
        }

        public async Task<FaturaDTO> UpdateStatusAsync(int faturaId, FaturaDurum yeniDurum)
        {
            var f = await _uow.Read<Fatura>().GetByIdAsync(faturaId);
            if (f == null) return null;

            f.Durum = yeniDurum;
            _uow.Update<Fatura>().Update(f);
            await _uow.CommitAsync();

            return _mapper.Map<FaturaDTO>(f);
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
