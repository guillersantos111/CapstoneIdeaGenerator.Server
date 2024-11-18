using CapstoneIdeaGenerator.Server.Entities.Models;

namespace CapstoneIdeaGenerator.Server.Services.Interfaces
{
    public interface IGeneratorService
    {
        Task<IEnumerable<string>> GetAllCategories();
        Task<Capstones> GetByProjectTypeAndCategory(string category, string projectType);
        Task<IEnumerable<string>> GetAllProjectTypes();
    }
}
