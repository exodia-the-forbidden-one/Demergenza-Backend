using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Demergenza.Domain.Entities.Admin;
using Demergenza.Application.Helpers.Configuration;

namespace Demergenza.Application.Helpers.Authentication
{

    public class TokenHelper
    {
        private readonly ConfigurationHelper _configuration;
        public TokenHelper(ConfigurationHelper configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Admin admin)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.TokenKey);
            var identity = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, admin.Username)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}