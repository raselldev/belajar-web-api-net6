using BelajarWebApi.Data;
using BelajarWebApi.Dtos;
using BelajarWebApi.Helper;
using BelajarWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BelajarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthController(DataContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginDto input)
        {
            var user = _context.Users.FirstOrDefault(e => e.Username == input.Username);
            if (user is null)
            {
                return BadRequest();
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, input.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return BadRequest();
            }

            if (passwordResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = _passwordHasher.HashPassword(user, input.Password);

                _context.Users.Update(user);
                _context.SaveChanges();
            }

            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:Key"];

            var accessToken = JwtBearerGenerator.Generate(user, issuer, audience, key);

            return Ok(new
            {
                AccessToken = accessToken
            });
        }
    }
}
