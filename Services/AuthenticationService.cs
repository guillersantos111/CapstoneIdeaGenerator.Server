using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using CapstoneIdeaGenerator.Server.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using CapstoneIdeaGenerator.Server.Data.DbContext;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

public class AuthenticationService : IAuthenticationService
{
    private readonly WebApplicationDbContext dbContext; 

    public AuthenticationService(WebApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public string Login(AuthRequest request)
    {
        var admin = dbContext.Admin.FirstOrDefault(a => a.Email == request.Email && a.Password == request.Password);

        if (admin == null)
        {
            return null;
        }

        string token = GenerateToken();
        admin.Token = token;
        dbContext.SaveChanges();

        return token;
    }

    public string ForgotPassword(string email)
    {
        var admin = dbContext.Admin.FirstOrDefault(a => a.Email == email);

        if (admin == null)
        {
            return null;
        }

        string resetToken = GenerateToken();
        admin.Token = resetToken;
        dbContext.SaveChanges();

        return resetToken;
    }

    private string GenerateToken()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] tokenBytes = new byte[32]; // 256-bit token
            rng.GetBytes(tokenBytes);
            return Convert.ToBase64String(tokenBytes);
        }
    }

    public bool Register(Admin admin)
    {
        if (dbContext.Admin.Any(a => a.Email == admin.Email))
        {
            return false;
        }

        admin.Token = GenerateToken();
        dbContext.Admin.Add(admin);
        dbContext.SaveChanges();
        return true;
    }

    public bool ResetPassword(string email, string newPassword, string resetToken)
    {
        var admin = dbContext.Admin.FirstOrDefault(u => u.Email == email && u.Token == resetToken);

        if (admin == null)
        {
            Console.WriteLine($"ResetPassword failed: No matching admin found for email {email} with token {resetToken}");
            return false;
        }

        admin.Password = newPassword;
        admin.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        dbContext.SaveChanges();

        return true;
    }

    public async Task<IEnumerable<Admin>> GetAllAccounts()
    {
        return await dbContext.Admin.ToListAsync();
    }

    public async Task<bool> RemoveAccount(int id)
    {
        var account = await dbContext.Admin.FindAsync(id);

        if (account == null)
        {
            return false;
        }

        dbContext.Admin.Remove(account);
        await dbContext.SaveChangesAsync();
        return true;
    }
}