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
        Task UpdateCategory(Category categoryTpBeUpdated, Category category);
        Task DeleteCategory(Category category);
        Task<bool> IsCategoryIdExist(int id);
    }
}