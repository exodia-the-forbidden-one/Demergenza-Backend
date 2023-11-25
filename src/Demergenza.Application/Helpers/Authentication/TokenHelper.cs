using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Application.Helpers.Configuration;
using Microsoft.Extensions.Configuration;

namespace Demergenza.Application.Helpers.Authentication
{

    public class TokenHelper
    {
        private readonly IConfiguration _configuration;
        public TokenHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Admin admin)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!);
            var identity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, admin.Username)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"],
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(15),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}