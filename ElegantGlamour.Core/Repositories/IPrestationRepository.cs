using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Repositories
{
    public interface IPrestationRepository : IRepository<Prestation>
    {
        Task<Prestation> GetPrestationByIdAsync(int id);
        Task<IReadOnlyList<Prestation>> GetPrestationsAsync();
        Task<IReadOnlyList<PrestationCategory>> GetPrestationCategoriesAsync();
        Task<bool> IsPrestationCategoryExistAsync(string id = null, string name = null);
    }
}