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
    /// Personel CRUD
    /// Bağımlı olduğu tablolar : Personel
    /// </summary>
    public interface IPersonelService
    {
        Task<PersonelDTO> GetByIdAsync(int id);
        Task<IEnumerable<PersonelDTO>> GetAllAsync();
        Task<IEnumerable<PersonelDTO>> FindAsync(Expression<Func<Personel, bool>> predicate);
        Task<PersonelDTO> CreateAsync(PersonelDTO dto);
        Task<PersonelDTO> UpdateAsync(PersonelDTO dto);
        Task<bool> DeleteAsync(int id);

       
    }
}
