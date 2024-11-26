using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using CapstoneIdeaGenerator.Server.Entities.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string GetMyName();
    }
}
