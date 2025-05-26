using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using MadrinHotelCRM.DTO.DTOModels;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.MVC.Models;
using MadrinHotelCRM.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MadrinHotelCRM.API.Controllers
{
    public class PersonelController : Controller
    {
        private readonly IPersonelService _personelService;

          private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public PersonelController(IPersonelService personelService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _personelService = personelService;
             _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var personel = await _personelService.GetByIdAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            return View(personel);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personelList = await _personelService.GetAllAsync();
            return View(personelList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonelDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _personelService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.PersonelId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonelDTO dto)
        {
            if (id != dto.PersonelId)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _personelService.UpdateAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _personelService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Find(Expression<Func<Personel, bool>> predicate)
        {
            var personelList = await _personelService.FindAsync(predicate);
            return View(personelList);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Email hatalı" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (result.Succeeded)
            {

                return Ok(new { token = GenerateJWTAsync(user) });
            }

            return Unauthorized();
        }

        private async Task<object> GenerateJWTAsync(AppUser user)
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
