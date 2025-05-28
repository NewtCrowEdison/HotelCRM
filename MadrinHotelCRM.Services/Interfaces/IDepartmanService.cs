using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Services.Interfaces
{
    public interface IDepartmanService
    {
        Task<IEnumerable<DepartmanDTO>> GetAllAsync();
        Task<DepartmanDTO> GetByIdAsync(int id);
        Task<DepartmanDTO> CreateAsync(DepartmanDTO dto);
        Task<DepartmanDTO> UpdateAsync(DepartmanDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
