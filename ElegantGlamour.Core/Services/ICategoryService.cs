using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> CreateCategory(Category newCategory);
        Task UpdatePrestation(Category categoryToBeUpdate);
        Task DeletePrestation(int id);
    }
}