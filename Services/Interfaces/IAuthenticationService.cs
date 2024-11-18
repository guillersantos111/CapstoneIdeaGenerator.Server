using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using Microsoft.AspNetCore.Identity.Data;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        bool Register(Admin admin);
        string Login(AuthRequest request);
        string ForgotPassword(string email);
        bool ResetPassword(string email, string newPassword, string resetToken);
        Task<IEnumerable<Admin>> GetAllAccounts();
        Task<bool> RemoveAccount(int id);
    }
}
