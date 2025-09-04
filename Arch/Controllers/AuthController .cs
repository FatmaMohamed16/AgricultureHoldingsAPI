using AmlakState.Data;
using AmlakState.DTO;
using Arch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Arch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ArchDbContext _context; 

        public AuthController(IConfiguration configuration, ArchDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
          
            var user = await _context.User
                .SingleOrDefaultAsync(u => u.UserName == loginDto.UserName);

            if (user == null)
            {
                return Unauthorized("اسم المستخدم غير صحيح.");
            }

          
            if (user.Password != loginDto.Password) 
            {
                return Unauthorized("كلمة المرور غير صحيحة.");
            }

         
            var token = GenerateJwtToken(user);

            return Ok(new { token });
        }

        


        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
    };

           
            claims.Add(new Claim("MarkazId", user.MarkazId?.ToString() ?? ""));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
