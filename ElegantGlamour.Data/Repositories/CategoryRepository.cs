using System.Threading.Tasks;
using ElegantGlamour.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ElegantGlamour.Core.Repositories;

namespace ElegantGlamour.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private ElegantGlamourDbContext ElegantGlamourDbContext
        {
            get { return Context as ElegantGlamourDbContext; }
        }

        public CategoryRepository(ElegantGlamourDbContext context)
            : base(context)
        { }

        public async Task<bool> IsCategoryIdExist(int id)
        {
            var isExist = false;
            var category = await ElegantGlamourDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category != null)
                isExist = true;

            return isExist;
        }
    }
}