using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> GetMyName();
        Task<IEnumerable<AdminDTO>> GetAllAccounts();
        Task<AdminDTO> RegisterAdmin(AdminRegisterDTO request);
        Task<string> LoginAdmin(AdminLoginDTO request);
        string CreateToken(Admins admin);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}