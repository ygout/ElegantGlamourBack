using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;

namespace ElegantGlamour.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }

        public async Task<Category> CreateCategory(Category newCategory)
        {
            await _unitOfWork.Categories.AddAsync(newCategory);
            await _unitOfWork.CommitAsync();

            return newCategory;
        }

        public Task DeletePrestation(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public Task<Category> GetCategoryById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePrestation(Category categoryToBeUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}