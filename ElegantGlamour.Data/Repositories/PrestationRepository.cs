using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Repositories;

namespace ElegantGlamour.Data.Repositories
{
    public class PrestationRepository : Repository<Prestation>, IPrestationRepository
    {
        private ElegantGlamourDbContext ElegantGlamourDbContext
        {
            get { return Context as ElegantGlamourDbContext; }
        }
        public PrestationRepository(ElegantGlamourDbContext context) : base(context) { }

        public async Task<IEnumerable<Prestation>> GetAllPrestationsWithCategoryAsync()
        {
            return await ElegantGlamourDbContext.Prestations
                            .Include(p => p.Category)
                            .ToListAsync();
        }

        public async Task<Prestation> GetPrestationWithCategoryByIdAsync(int id)
        {
            return await ElegantGlamourDbContext.Prestations
                    .Include(p => p.Category)
                    .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}