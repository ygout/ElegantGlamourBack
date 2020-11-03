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

namespace ElegantGlamour.API.Controllers
{

    public class PrestationsController : BaseApiController
    {
        private readonly IPrestationService _prestationService;
        private readonly IMapper _mapper;
        private readonly ILogger<PrestationsController> _logger;
        public PrestationsController(IPrestationService prestationService, IMapper mapper, ILogger<PrestationsController> logger)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._prestationService = prestationService;

        }
        /// <summary>
        /// Create a new prestation
        /// </summary>
        /// <param name="newPrestation"></param>
        /// <returns>A newly prestation created</returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationDto>), Status201Created)]
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationDto>), Status400BadRequest)]

        public async Task<ApiResponse> CreatePrestation([FromBody] AddPrestationDto newPrestation)
        {
            var validator = new AddPrestationDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(newPrestation);

                if (!validationResult.IsValid)
                    throw new ApiException(validationResult);

                var prestationToCreate = _mapper.Map<AddPrestationDto, Prestation>(newPrestation);
                var newPrestationCreated = await _prestationService.CreatePrestation(prestationToCreate);

                var getPrestationDto = _mapper.Map<Prestation, GetPrestationDto>(newPrestationCreated);

                return new ApiResponse("La prestation a été correctement crée", getPrestationDto, Status201Created);
            }
            catch (CategoryDoesNotExistException ex)
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
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationDto>), Status201Created)]

        public async Task<ApiResponse> UpdatePrestation(int id, [FromBody] UpdatePrestationDto updatePrestationDto)
        {
            var validator = new UpdatePrestationDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(updatePrestationDto);

                if (id == 0 || !validationResult.IsValid)
                    throw new ApiException(validationResult);

                var prestationToBeUpdate = await _prestationService.GetPrestationById(id);

                if (id == 0 || prestationToBeUpdate == null)
                    throw new ApiException(ErrorMessage.Err_Prestation_Id_Does_Not_Exist, Status404NotFound);

                var prestation = _mapper.Map<UpdatePrestationDto, Prestation>(updatePrestationDto);
                await _prestationService.UpdatePrestation(prestationToBeUpdate, prestation);

                var updatedPrestation = await _prestationService.GetPrestationById(id);

                var updatedPrestationDto = _mapper.Map<Prestation, GetPrestationDto>(updatedPrestation);

                return new ApiResponse($"La prestation avec pour id: {id} a été correctement modifié", updatedPrestationDto, Status201Created);

            }
            catch (CategoryDoesNotExistException ex)
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
        [HttpGet("")]
        [ProducesResponseType(typeof(ResponseWrapper<IEnumerable<GetPrestationDto>>), Status200OK)]

        public async Task<IEnumerable<GetPrestationDto>> GetPrestations([FromQuery]PrestationSpecParams spec)
        {
            try
            {
                IEnumerable<Prestation> prestations = await this._prestationService.GetAllPrestations(spec);
                IEnumerable<GetPrestationDto> prestationsDtos = this._mapper.Map<IEnumerable<Prestation>, IEnumerable<GetPrestationDto>>(prestations);

                return prestationsDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseWrapper<GetPrestationDto>), Status200OK)]

        public async Task<GetPrestationDto> GetPrestationById(int id)
        {
            try
            {
                var prestation = await this._prestationService.GetPrestationById(id);
                var prestationDto = this._mapper.Map<Prestation, GetPrestationDto>(prestation);

                if (prestationDto == null || id == 0)
                    throw new ApiException(ErrorMessage.Err_Prestation_Id_Does_Not_Exist, Status404NotFound);

                return prestationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseWrapper<bool>), Status200OK)]

        public async Task<ApiResponse> DeletePrestation(int id)
        {
            try
            {
                var prestationToBeDeleted = await _prestationService.GetPrestationById(id);

                if (prestationToBeDeleted == null)
                    throw new ApiException(ErrorMessage.Err_Prestation_Id_Does_Not_Exist, Status404NotFound);

                await _prestationService.DeletePrestation(prestationToBeDeleted);

                return new ApiResponse("Prestation has been deleted ", id, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(ResponseWrapper<IEnumerable<GetPrestationCategoryDto>>), Status200OK)]

        public async Task<IEnumerable<GetPrestationCategoryDto>> GetPrestationCategories([FromQuery]PrestationCategorySpecParams spec)
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

        [HttpGet("categories/{id}")]
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

        [HttpPost("categories")]
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

        [HttpPut("categories/{id}")]
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
        [HttpDelete("categories/{id}")]
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