using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Repositories;

namespace ElegantGlamour.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ElegantGlamourDbContext context) : base(context) { }
    }
}