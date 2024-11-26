using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using Microsoft.AspNetCore.Mvc;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CapstoneIdeaGenerator.Server.Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CapstoneIdeaGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public static Admins admin = new Admins();
        private readonly IConfiguration configuration;
        private readonly IAuthenticationService authenticationService;
        private readonly WebApplicationDbContext dbContext;

        public AuthenticationController(IAuthenticationService authenticationService, IConfiguration configuration, WebApplicationDbContext dbContext)
        {
            this.authenticationService = authenticationService;
            this.configuration = configuration;
            this.dbContext = dbContext;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var userName = authenticationService.GetMyName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Admins>> Register(AdminRegisterDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var admin = new Admins
            {
                AdminId = request.AdminId,
                Name = request.Name,
                Gender = request.Gender,
                Age = request.Age,
                Email = request.Email,
                DateJoined = DateTime.UtcNow,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            dbContext.Admins.Add(admin);
            await dbContext.SaveChangesAsync();

            return admin;
        }



        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AdminLoginDTO request)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);

            if (admin == null)
            {
                return BadRequest("Admin Not Found");
            }

            if (!VerifyPasswordHash(request.Password, admin.PasswordHash, admin.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(admin);
            return Ok(token);
        }

        [HttpGet("accounts")]
        public async Task<ActionResult<IEnumerable<Admins>>> GetAllAccounts()
        {
            try
            {
                var accounts = await authenticationService.GetAllAccounts();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }



        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


        private string CreateToken(Admins admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
    }
}
