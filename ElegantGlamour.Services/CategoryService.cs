using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core;
using System;
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
            try
            {
                await _unitOfWork.Categories.AddAsync(newCategory);
                await _unitOfWork.CommitAsync();

                return newCategory;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteCategory(Category category)
        {
            try
            {
                _unitOfWork.Categories.Remove(category);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                return await _unitOfWork.Categories.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            try
            {
                return await _unitOfWork.Categories.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateCategory(Category categoryToBeUpdated, Category category)
        {
            try
            {
                categoryToBeUpdated.Title = category.Title;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsCategoryIdExist(int id)
        {
            try
            {
                return await _unitOfWork.Categories.IsCategoryIdExist(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}