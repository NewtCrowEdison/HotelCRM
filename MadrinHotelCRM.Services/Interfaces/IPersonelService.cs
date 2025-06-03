// Services/Interfaces/IPersonelService.cs
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;

namespace MadrinHotelCRM.Services.Interfaces
{
    public interface IPersonelService
    {
        Task<PersonelDTO> GetByIdAsync(int id);
        Task<IEnumerable<PersonelDTO>> GetAllAsync();
        Task<IEnumerable<PersonelDTO>> FindAsync(Expression<Func<MadrinHotelCRM.Entities.Models.Personel, bool>> predicate);
        Task<PersonelDTO> CreateAsync(PersonelDTO dto);
        Task<PersonelDTO> UpdateAsync(PersonelDTO dto);
        Task<bool> DeleteAsync(int id);
        //şifre değişimi
        Task<PersonelDTO> GetByKullaniciIdAsync(string kullaniciId);
        Task<bool> ChangePasswordAsync(ChangePasswordDTO dto);
        

    }
}
