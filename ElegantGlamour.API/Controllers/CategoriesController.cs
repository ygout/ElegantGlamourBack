using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.Api.Validators;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElegantGlamour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this._mapper = mapper;
            this._categoryService = categoryService;

        }

        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GetCategoryDto>>>> GetCategories()
        {
            IEnumerable<Category> categories = await this._categoryService.GetAllCategories();
            var categoriesDto = this._mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpPost("")]
        public async Task<ActionResult<ApiResponse<GetCategoryDto>>> CreateCategory([FromBody] AddCategoryDto addCategoryDto)
        {
            var validator = new AddCategoryDtoValidator();
            var validationResult = await validator.ValidateAsync(addCategoryDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining

            var categoryToCreate = _mapper.Map<AddCategoryDto, Category>(addCategoryDto);

            var newCategory = await _categoryService.CreateCategory(categoryToCreate);

            var categoryCreated = await _categoryService.GetCategoryById(newCategory.Id);

            var getCategoryDto = _mapper.Map<Category, GetCategoryDto>(categoryCreated);

            return Ok(getCategoryDto);
        }

    }
}