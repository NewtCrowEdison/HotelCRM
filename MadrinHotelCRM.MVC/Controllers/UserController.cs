using MadrinHotelCRM.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MadrinHotelCRM.MVC.Controllers
{
    public class UserController : Controller
    {
     private readonly SignInManager<AppUser> _signInManager;
       private readonly UserManager<AppUser> _userManager;
       private readonly IConfiguration _configuration;
       
        
        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel){

           var user = await _userManager.FindByEmailAsync(loginModel.Email);
              
            if (user == null)
                {
                    return BadRequest(new { message = "Email hatalı" });
                }

            var result  = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password,false);

            if(result.Succeeded){

                return Ok(new {token = GenerateJWTAsync(user)});
            }
            
            return Unauthorized();

        }

        private async Task<object> GenerateJWTAsync( AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
             var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"] ?? "");

                    
                  var roles = await _userManager.GetRolesAsync(user);

                     var claims = new List<Claim>
                   {
                         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                         new Claim(ClaimTypes.Name, user.UserName ?? "")
                    };

                  
                 foreach (var role in roles)
                 {
                         claims.Add(new Claim(ClaimTypes.Role, role));
                  }

                   var tokenDescriptor = new SecurityTokenDescriptor
                   {
                     Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                     SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                   };

                  var token = tokenHandler.CreateToken(tokenDescriptor);
                  return tokenHandler.WriteToken(token);

        }
    }



}