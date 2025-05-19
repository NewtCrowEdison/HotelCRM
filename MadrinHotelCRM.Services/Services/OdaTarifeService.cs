using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MadrinHotelCRM.Services.Services
{
    public class OdaTarifeService : IOdaTarifeService
    {
        private readonly IGenericRepository<OdaTarife> _odaTarifeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OdaTarifeService(
            IGenericRepository<OdaTarife> odaTarifeRepo,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _odaTarifeRepo = odaTarifeRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OdaTarifeDTO> AddAsync(OdaTarifeDTO odaTarifeDTO)
        {
           var entity =  _mapper.Map<OdaTarife>(odaTarifeDTO);
            await _odaTarifeRepo.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<OdaTarifeDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int odaId, int tarifeId)
        {
            // OdaTarife nesnesini silmeden önce odaId ve tarifeId'yi kontrol et
            var odaTarife =await _odaTarifeRepo.GetAllAsync();
             var typeOdaTarife = odaTarife.FirstOrDefault(x => x.OdaId == odaId && x.TarifeId == tarifeId);
               
            if (typeOdaTarife == null)
            {
                throw new ArgumentException("OdaTarife bulunamadı.");
            }

            
            _odaTarifeRepo.Delete(typeOdaTarife);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<IEnumerable<OdaTarifeDTO>> GetAllAsync()
        {
            var list = await _odaTarifeRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OdaTarifeDTO>>(list);
        }

        public async Task<IEnumerable<OdaTarifeDTO>> GetByOdaIdAsync(int odaId)
        {
            var oda = await _odaTarifeRepo.GetAllAsync();
            var odaTarifeList = oda.Where(x => x.OdaId == odaId).ToList();
            if (odaTarifeList == null || odaTarifeList.Count == 0)
            {
                throw new ArgumentException("OdaTarife bulunamadı.");
            }
            return _mapper.Map<IEnumerable<OdaTarifeDTO>>(odaTarifeList);
        }

        public async Task<IEnumerable<OdaTarifeDTO>> GetByTarifeIdAsync(int tarifeId)
        {
            var oda = await _odaTarifeRepo.GetAllAsync();
            var odaTarifeList = oda.Where(x => x.TarifeId == tarifeId).ToList();
            if (odaTarifeList == null || odaTarifeList.Count == 0)
            {
                throw new ArgumentException("OdaTarife bulunamadı.");
            }
            return _mapper.Map<IEnumerable<OdaTarifeDTO>>(odaTarifeList);
        }

        public async Task<OdaTarifeDTO> GetDetailsAsync(int odaId, int tarifeId)
        {
            var odaTarife = await _odaTarifeRepo.GetAllAsync();
            var typeOdaTarife = odaTarife.FirstOrDefault(x => x.OdaId == odaId && x.TarifeId == tarifeId);
            if (typeOdaTarife == null)
            {
                throw new ArgumentException("OdaTarife bulunamadı.");
            }
            return _mapper.Map<OdaTarifeDTO>(typeOdaTarife);
        }

       
    }
}