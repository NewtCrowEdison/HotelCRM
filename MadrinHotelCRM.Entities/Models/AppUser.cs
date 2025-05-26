using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MadrinHotelCRM.Entities.Models
{
    public class AppUser : IdentityUser//<string>
    {
         //public int Id { get; set; }
         //public string Email { get; set; }
         //public string PasswordHash { get; set; } // hashlenmiş şifre
         //public string Role { get; set; } // "Admin", "Personel" vs.
    }
}
