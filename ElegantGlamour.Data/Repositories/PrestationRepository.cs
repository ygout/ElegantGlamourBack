using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Repositories;
using System;

namespace ElegantGlamour.Data.Repositories
{
    public class PrestationRepository : Repository<Prestation>, IPrestationRepository
    {
        private ElegantGlamourDbContext ElegantGlamourDbContext
        {
            get { return _context as ElegantGlamourDbContext; }
        }
        public PrestationRepository(ElegantGlamourDbContext context) : base(context) { }

        public async Task<Prestation> GetPrestationByIdAsync(int id)
        {
            return await ElegantGlamourDbContext.Prestations
                   .Include(p => p.PrestationCategory)
                   .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Prestation>> GetPrestationsAsync()
        {
            return await ElegantGlamourDbContext.Prestations
                            .Include(p => p.PrestationCategory)
                            .ToListAsync();

        }

        public async Task<IReadOnlyList<PrestationCategory>> GetPrestationCategoriesAsync()
        {
            return await ElegantGlamourDbContext.PrestationCategories.ToListAsync();
        }

        public async Task<bool> IsPrestationCategoryExistAsync(string id = null, string name = null)
        {
            var isExist = false;

            if (!string.IsNullOrEmpty(id) || await ElegantGlamourDbContext.PrestationCategories.AnyAsync(x => x.Id == Int32.Parse(id)))
                isExist = true;
            
            if(!string.IsNullOrEmpty(name) || await ElegantGlamourDbContext.PrestationCategories.AnyAsync(x => x.Name == name))
                isExist = true;

            return isExist;
        }
    }
}