using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Services
{
    /// <summary>
    /// Oda tarifeleriyle ilgili işlemleri tanımlayan servis arayüzü.
    /// </summary>
    public interface IOdaTarifeService
    {
          
          Task<IEnumerable<OdaTarifeDTO>> GetAllAsync();
          Task<OdaTarifeDTO> AddAsync(OdaTarifeDTO odaTarifeDTO );
          
          Task<bool> DeleteAsync(int odaId, int tarifeId);
          
          Task<IEnumerable<OdaTarifeDTO>> GetByOdaIdAsync(int odaId);
          Task<IEnumerable<OdaTarifeDTO>> GetByTarifeIdAsync(int tarifeId);
          Task<OdaTarifeDTO> GetDetailsAsync(int odaId, int tarifeId);
    }
}