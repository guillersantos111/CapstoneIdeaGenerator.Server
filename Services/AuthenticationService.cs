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
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

public class AuthenticationService : IAuthenticationService
{
    private readonly WebApplicationDbContext dbContext;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthenticationService(WebApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        this.dbContext = dbContext;
        this.httpContextAccessor = httpContextAccessor;
    }

    public string GetMyName()
    {
        var result = string.Empty;
        if (httpContextAccessor.HttpContext != null)
        {
            result = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
        return result;
    }
}