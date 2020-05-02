using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using ElegantGlamour.API.Data;
using ElegantGlamour.API.Dtos;
using ElegantGlamour.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElegantGlamour.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _autoMapper;
        public CategoryService(DataContext context, IMapper autoMapper)
        {
            _autoMapper = autoMapper;
            _context = context;

        }
        public async Task<ServiceResponse<GetCategoryDto>> CreateCategory(AddCategoryDto categoryDto)
        {
            ServiceResponse<GetCategoryDto> serviceResponse = new ServiceResponse<GetCategoryDto>();
            Category categoryDb = this._autoMapper.Map<Category>(categoryDto);

            await _context.AddAsync(categoryDb);
            await _context.SaveChangesAsync();

            Category getCategoryFromDb = _context.Categories.FirstOrDefault(c => c.Id == categoryDb.Id);
            serviceResponse.Data = _autoMapper.Map<GetCategoryDto>(getCategoryFromDb);
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<bool>> DeleteCategory(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetCategoryDto>>> GetAllCategories()
        {
            ServiceResponse<List<GetCategoryDto>> serviceResponse = new ServiceResponse<List<GetCategoryDto>>();
            
            serviceResponse.Data = await this._context.Categories.Select(c => this._autoMapper.Map<GetCategoryDto>(c)).ToListAsync();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> GetCategoryById(int id)
        {
            ServiceResponse<GetCategoryDto> serviceResponse = new ServiceResponse<GetCategoryDto>();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _autoMapper.Map<GetCategoryDto>(category);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCategoryDto>> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            throw new System.NotImplementedException();
        }
    }
}