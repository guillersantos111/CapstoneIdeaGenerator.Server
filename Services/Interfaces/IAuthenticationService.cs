using CapstoneIdeaGenerator.Server.Entities.AuthenticationModels;
using Microsoft.AspNetCore.Identity.Data;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string GetMyName();
    }
}
