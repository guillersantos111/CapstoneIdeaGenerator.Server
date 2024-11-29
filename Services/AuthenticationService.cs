using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using CapstoneIdeaGenerator.Server.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CapstoneIdeaGenerator.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly WebApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public AuthenticationService(WebApplicationDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<string> GetMyName()
        {
            return await Task.FromResult("Admin");
        }

        public async Task<IEnumerable<AdminDTO>> GetAllAccounts()
        {
            var accounts = await dbContext.Admins
                .Select(a => new AdminDTO
                {
                    Name = a.Name,
                    Email = a.Email,
                    Age = a.Age,
                    Gender = a.Gender,
                    DateJoined = a.DateJoined
                })
                .ToListAsync();

            return accounts;
        }


        public async Task<AdminDTO> RegisterAdmin(AdminRegisterDTO request)
        {
            var existingAdmin = await dbContext.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (existingAdmin != null)
                throw new Exception("Admin with this email already exists.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var admin = new Admins
            {
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

            return new AdminDTO
            {
                Name = admin.Name,
                Gender = admin.Gender,
                Age = admin.Age,
                DateJoined = admin.DateJoined,
                Email = admin.Email
            };
        }


        public async Task<string> LoginAdmin(AdminLoginDTO request)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (admin == null)
            { 
                throw new Exception("Admin Not Found"); 
            }

            if (!VerifyPasswordHash(request.Password, admin.PasswordHash, admin.PasswordSalt))
            {
                throw new Exception("Incorrect Password"); 
            }

            return CreateToken(admin);
        }


        public async Task ResetPassword(string token, string newPassword)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(a => a.PasswordResetToken == token && a.ResetTokenExpiry > DateTime.UtcNow);
            if (admin == null)
            {
                throw new Exception("Invalid Or Expired Reset Token");
            }

            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            admin.PasswordHash = passwordHash;
            admin.PasswordSalt = passwordSalt;
            admin.PasswordResetToken = null;
            admin.ResetTokenExpiry = null;

            dbContext.Admins.Update(admin);
            await dbContext.SaveChangesAsync();
        }


        public async Task<string> GeneratePasswordResetToken(AdminForgotPasswordDTO request)
        {
            var admin = await dbContext.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (admin == null)
            {
                throw new Exception("Admin Not Found");
            }

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            admin.PasswordResetToken = token;
            admin.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            dbContext.Admins.Update(admin);
            await dbContext.SaveChangesAsync();

            return token;
        }


        public string CreateToken(Admins admin)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }


        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}