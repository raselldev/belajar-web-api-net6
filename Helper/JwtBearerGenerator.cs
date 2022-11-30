using BelajarWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BelajarWebApi.Helper
{
    public class JwtBearerGenerator
    {
        public static string Generate(User user, string issuer, string audience, string key)
        {
            // siapkan klaim digunakan untuk identity service 
            var claims = new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.GivenName, user.Fullname)
            };

            // pakai symmectric security key
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddDays(1),
                IssuedAt = DateTime.UtcNow,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
