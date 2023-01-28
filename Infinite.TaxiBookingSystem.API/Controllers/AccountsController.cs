using Infinite.TaxiBookingSystem.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public AccountsController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Login(LoginModel login) 
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.UserName == login.UserName && x.Password == login.Password);
            if(currentUser == null)
            {
                return NotFound("Invalud username or password");
            }
            var token = GenerateToken(currentUser);
            if(token == null)
            {
                return NotFound("Invalid Credentials");
            }
            return Ok(token);
        }

        [NonAction]
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Email, user.EmailId)
            };

            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
                //audience: _configuration["JWT:audience"],
                claims: myClaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials
                );
            
            var tokens = new JwtSecurityTokenHandler().WriteToken(token);

            return tokens;
        }
    }
}
