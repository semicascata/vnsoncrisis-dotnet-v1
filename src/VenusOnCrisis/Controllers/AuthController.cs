using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenusOnCrisis.Data;
using VenusOnCrisis.DTOs;
using VenusOnCrisis.Entities;
using VenusOnCrisis.Interfaces;

namespace VenusOnCrisis.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;
        public AuthController(DataContext context, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // validations
            var userExists = await UserExists(registerDto.Username);
            if(userExists)
            {
                return BadRequest("User already exists");
            }

            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDto.Username.ToLower(),
                PassHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PassSalt = hmac.Key,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto 
            {
                Username = user.UserName,
                Token = _jwtService.CreateToken(user),
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(cb => cb.UserName == loginDto.Username);
            
            if(user == null) 
            {
                return Unauthorized("Invalid user");
            }

            using var hmac = new HMACSHA512(user.PassSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PassHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _jwtService.CreateToken(user),
            };
        }

        // util validation, user exists on db
        private async Task<bool> UserExists(string username)
        {
            var user = await _context.Users.AnyAsync(cb => cb.UserName == username.ToLower());
            return user;
        }
    }
}