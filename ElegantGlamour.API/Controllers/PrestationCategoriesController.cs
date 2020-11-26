using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.Api.Validators;
using ElegantGlamour.Api.Dtos;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.Extensions.Logging;
using System.Reflection;
using AutoWrapper.Wrappers;
using ElegantGlamour.Core.Error;
using ElegantGlamour.Api.Swagger;
using Swashbuckle.AspNetCore.Annotations;
using ElegantGlamour.Core.Specifications;
using ElegantGlamour.API.Controllers;

namespace ElegantGlamour.Api.Controllers
{
    [Route("api/prestation-categories")]
    public class PrestationCategoriesController : BaseApiController
    {
        private readonly IPrestationService _prestationService;
        private readonly IMapper _mapper;
        private readonly ILogger<PrestationCategoriesController> _logger;

        public PrestationCategoriesController(IPrestationService prestationService, IMapper mapper, ILogger<PrestationCategoriesController> logger)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._prestationService = prestationService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationCategoryDto>), Status200OK)]

        public async Task<GetPrestationCategoryDto> GetPrestationCategoryById(int id)
        {
            try
            {
                var category = await this._prestationService.GetPrestationCategoryById(id);
                var categoryDto = this._mapper.Map<PrestationCategory, GetPrestationCategoryDto>(category);
                if (categoryDto == null)
                    throw new ApiException($"La categorie avec pour id: {id} n'existe pas", Status404NotFound);

                return categoryDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(ResponseWrapper<IEnumerable<GetPrestationCategoryDto>>), Status200OK)]

        public async Task<IEnumerable<GetPrestationCategoryDto>> GetPrestationCategories([FromQuery] PrestationCategorySpecParams spec)
        {
            try
            {
                IEnumerable<PrestationCategory> categories = await this._prestationService.GettAllPrestationCategories(spec);
                IEnumerable<GetPrestationCategoryDto> categoriesDto = this._mapper.Map<IEnumerable<PrestationCategory>, IEnumerable<GetPrestationCategoryDto>>(categories);

                return categoriesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }

        }

        [HttpPost("")]
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationCategoryDto>), Status201Created)]
        public async Task<ApiResponse> CreatePrestationCategory([FromBody] AddPrestationCategoryDto addCategoryDto)
        {
            var validator = new AddCategoryDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(addCategoryDto);

                if (!validationResult.IsValid)
                    throw new ApiException(validationResult); // this needs refining

                var categoryToCreate = _mapper.Map<AddPrestationCategoryDto, PrestationCategory>(addCategoryDto);

                var newCategory = await _prestationService.CreatePrestationCategory(categoryToCreate);

                var categoryCreated = await _prestationService.GetPrestationCategoryById(newCategory.Id);

                var getCategoryDto = _mapper.Map<PrestationCategory, GetPrestationCategoryDto>(categoryCreated);

                return new ApiResponse("La catégorie a été correctement crée", getCategoryDto, Status201Created);
            }
            catch (CategoryAlreadyExistException ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);

                var apiException = new ApiException(ex.Message, Status400BadRequest);
                apiException.CustomError = ex.Message;
                throw apiException;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationCategoryDto>), Status201Created)]

        public async Task<ApiResponse> UpdatePrestationCategory(int id, [FromBody] UpdatePrestationCategoryDto updateCategoryDto)
        {
            var validator = new UpdateCategoryDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(updateCategoryDto);

                if (!validationResult.IsValid)
                    throw new ApiException(validationResult);

                var categoryToBeUpdate = await _prestationService.GetPrestationCategoryById(id);

                if (id == 0 || categoryToBeUpdate == null)
                    throw new ApiException("La catégorie n'existe pas", Status404NotFound);

                var category = _mapper.Map<UpdatePrestationCategoryDto, PrestationCategory>(updateCategoryDto);

                await _prestationService.UpdatePrestationCategory(categoryToBeUpdate, category);

                var updatedCategory = await _prestationService.GetPrestationCategoryById(id);
                var updatedCategoryDto = _mapper.Map<PrestationCategory, GetPrestationCategoryDto>(updatedCategory);

                return new ApiResponse($"La catégorie avec pour id: {id} a été correctement modifié", updatedCategoryDto, Status201Created);
            }
            catch (CategoryAlreadyExistException ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);

                var apiException = new ApiException(ex.Message, Status400BadRequest);
                apiException.CustomError = ex.Message;
                throw apiException;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
        [HttpDelete("{id}")]
        [SwaggerResponse(Status200OK, "La catégorie avec pour id: {id} a été correctement supprimé", typeof(ResponseWrapper<bool>))]
        // [ProducesResponseType(typeof(ApiException), Status404NotFound)]
        public async Task<ApiResponse> DeletePrestationCategory(int id)
        {
            try
            {
                var categoryToBeDeleted = await _prestationService.GetPrestationCategoryById(id);

                if (categoryToBeDeleted == null)
                    throw new ApiException("La catégorie n'existe pas", Status404NotFound);

                await _prestationService.DeletePrestationCategory(categoryToBeDeleted);

                return new ApiResponse($"La catégorie avec pour id: {id} a été correctement supprimé.", true);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }


        }
    }
}