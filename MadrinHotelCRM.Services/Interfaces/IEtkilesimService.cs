using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Interfaces
{
    /// <summary>
    /// Müşteriyle olan iletişimi takip etmek
    /// Bağımlı olduğu tablolar : MusteriEtkilesim
    /// </summary>
    public interface IEtkilesimService
    {
      
        Task<MusteriEtkilesimDTO> GetByIdAsync(int id);
        Task<IEnumerable<MusteriEtkilesimDTO>> GetAllAsync();

        // Dinamik filtreleme: entity seviyesinde expression alıp DTO'ya map eder
        Task<IEnumerable<MusteriEtkilesimDTO>> FindAsync(Expression<Func<MusteriEtkilesim, bool>> predicate);
        Task<MusteriEtkilesimDTO> CreateAsync(MusteriEtkilesimDTO dto);
        Task<MusteriEtkilesimDTO> UpdateAsync(MusteriEtkilesimDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
