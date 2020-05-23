using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.Api.Responses;
using ElegantGlamour.Api.Validators;
using ElegantGlamour.Core.Dtos;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;
using ElegantGlamour.Api.Extensions;
using System;
using Microsoft.Extensions.Logging;
using System.Reflection;
using AutoWrapper.Wrappers;

namespace ElegantGlamour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ICategoryService categoryService, IMapper mapper, ILogger<CategoriesController> logger)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._categoryService = categoryService;

        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<GetCategoryDto>>> GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = await this._categoryService.GetAllCategories();
                IEnumerable<GetCategoryDto> categoriesDto = this._mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDto>>(categories);

                return Ok(categoriesDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
        {
            try
            {
                var category = await this._categoryService.GetCategoryById(id);
                var categoryDto = this._mapper.Map<Category, GetCategoryDto>(category);
                
                if(categoryDto == null)
                    return NotFound();
    
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<GetCategoryDto>> CreateCategory([FromBody] AddCategoryDto addCategoryDto)
        {
            var validator = new AddCategoryDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(addCategoryDto);

                if (!validationResult.IsValid)
                    throw new ApiException(validationResult.Errors); // this needs refining

                var categoryToCreate = _mapper.Map<AddCategoryDto, Category>(addCategoryDto);

                var newCategory = await _categoryService.CreateCategory(categoryToCreate);

                var categoryCreated = await _categoryService.GetCategoryById(newCategory.Id);

                var getCategoryDto = _mapper.Map<Category, GetCategoryDto>(categoryCreated);

                return Ok(getCategoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetCategoryDto>> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            var validator = new UpdateCategoryDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(updateCategoryDto);

                if (id == 0 || !validationResult.IsValid)
                     throw new ApiException(validationResult.Errors);

                var categoryToBeUpdate = await _categoryService.GetCategoryById(id);

                if (categoryToBeUpdate == null)
                    return NotFound();

                var category = _mapper.Map<UpdateCategoryDto, Category>(updateCategoryDto);

                await _categoryService.UpdateCategory(categoryToBeUpdate, category);

                var updatedCategory = await _categoryService.GetCategoryById(id);
                var updatedCategoryDto = _mapper.Map<Category, GetCategoryDto>(updatedCategory);

                return Ok(updateCategoryDto);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteCategory(int id)
        {
            var response = new Response();
            try
            {
                var categoryToBeDeleted = await _categoryService.GetCategoryById(id);

                if (categoryToBeDeleted == null)
                    return NotFound();

                await _categoryService.DeleteCategory(categoryToBeDeleted);

                return new ApiResponse("Category has been deleted in the database.", id, 200);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }

  
        }
    }
}