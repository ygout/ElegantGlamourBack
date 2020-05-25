using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Repositories
{
    public interface IPrestationRepository : IRepository<Prestation>
    {
        Task<IEnumerable<Prestation>> GetAllPrestationsWithCategoryAsync();
        Task<Prestation> GetPrestationWithCategoryByIdAsync(int id);
    }
}