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
    public class OdaService : IOdaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public OdaService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OdaDTO>> GetAllAsync()
        {
            try
            {
                var odalar = (await _uow.Read<Oda>().GetAllAsync()).ToList();

                var result = new List<OdaDTO>(odalar.Count);
                foreach (var oda in odalar)
                {
                    var dto = _mapper.Map<OdaDTO>(oda);

                    var tipiEntity = await _uow.Read<OdaTipi>()
                                               .GetByIdAsync(oda.OdaTipiId);

                    if (tipiEntity != null)
                    {
                        dto.OdaTipi = _mapper.Map<OdaTipiDTO>(tipiEntity);
                        dto.OdaTipiAdi = tipiEntity.OdaTurAd; //  DTO’ya adı burada veriliyor
                    }
                    else
                    {
                        dto.OdaTipiAdi = "Tanımsız";
                    }

                    result.Add(dto);
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("💥 GetAllAsync patladı: " + ex.Message);
                throw;
            }
        }



        public async Task<OdaDTO> GetByIdAsync(int id)
        {
            var oda = await _uow.Read<Oda>().GetByIdAsync(id);
            if (oda == null) return null;

            var dto = _mapper.Map<OdaDTO>(oda);
            var tipiEntity = await _uow.Read<OdaTipi>()
                                      .GetByIdAsync(oda.OdaTipiId);
            dto.OdaTipi = _mapper.Map<OdaTipiDTO>(tipiEntity);
            return dto;
        }

        public async Task<IEnumerable<OdaDTO>> FindAsync(Expression<Func<Oda, bool>> predicate)
        {
            var odalar = (await _uow.Read<Oda>().FindAsync(predicate)).ToList();

            var result = new List<OdaDTO>(odalar.Count);
            foreach (var oda in odalar)
            {
                var dto = _mapper.Map<OdaDTO>(oda);
                var tipiEntity = await _uow.Read<OdaTipi>()
                                          .GetByIdAsync(oda.OdaTipiId);
                dto.OdaTipi = _mapper.Map<OdaTipiDTO>(tipiEntity);
                result.Add(dto);
            }

            return result;
        }

        public async Task<OdaDTO> CreateAsync(OdaDTO dto)
        {
            var entity = _mapper.Map<Oda>(dto);
            entity.OdaTipi = null;
            await _uow.Create<Oda>().AddAsync(entity);
            await _uow.CommitAsync();

            // Yeni eklenen kaydı tekrar çek ve DTO’yu nav-prop ile doldur
            return await GetByIdAsync(entity.OdaId);
        }

        public async Task<OdaDTO> UpdateAsync(OdaDTO dto)
        {
            var mevcut = await _uow.Read<Oda>().GetByIdAsync(dto.OdaId);
            if (mevcut == null) return null;

            _mapper.Map(dto, mevcut);
            _uow.Update<Oda>().Update(mevcut);
            await _uow.CommitAsync();

            return await GetByIdAsync(dto.OdaId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _uow.Read<Oda>().GetByIdAsync(id);
            if (entity == null) return false;

            _uow.Delete<Oda>().Delete(entity);
            await _uow.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateRoomStatusAsync(int odaId, string yeniDurum)
        {
            var oda = await _uow.Read<Oda>().GetByIdAsync(odaId);
            if (oda == null) return false;

            if (Enum.TryParse(yeniDurum, out Entities.Enums.OdaDurum durum))
            {
                oda.Durum = durum;
                _uow.Update<Oda>().Update(oda);
                await _uow.CommitAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateTariffAsync(int odaId, int tarifeId)
        {
            var oda = await _uow.Read<Oda>().GetByIdAsync(odaId);
            if (oda == null) return false;

            oda.OdaTipiId = tarifeId;
            _uow.Update<Oda>().Update(oda);
            await _uow.CommitAsync();
            return true;
        }
    }
}


