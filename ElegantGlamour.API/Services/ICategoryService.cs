using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.API.Dtos;
using ElegantGlamour.API.Models;

namespace ElegantGlamour.API.Services
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories();
        Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id);
        Task<ServiceResponse<GetCategoryDto>> CreateCategory(AddCategoryDto newCategory);
        Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto newCategory);
        Task<ServiceResponse<bool>> DeleteCategory(int id);
    }
}