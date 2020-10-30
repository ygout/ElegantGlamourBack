using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Specifications;

namespace ElegantGlamour.Core.Repositories
{
    public interface IPrestationRepository : IRepository<Prestation>
    {
        Task<Prestation> GetPrestationByIdAsync(int id);
        Task<IReadOnlyList<Prestation>> GetPrestationsAsync();
        Task<PrestationCategory> GetPrestationCategoryAsync(ISpecification<PrestationCategory> spec);
        Task<IReadOnlyList<PrestationCategory>> GetPrestationCategoriesAsync(ISpecification<PrestationCategory> spec);
        Task<bool> IsPrestationCategoryExistAsync(string id = null, string name = null);
    }
}