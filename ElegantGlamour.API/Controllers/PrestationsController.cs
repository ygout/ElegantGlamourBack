using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.Api.Validators;
using ElegantGlamour.Core.Dtos;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.Extensions.Logging;
using System.Reflection;
using AutoWrapper.Wrappers;

namespace ElegantGlamour.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestationsController : ControllerBase
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

        [HttpPost("")]
        public async Task<ActionResult<GetPrestationDto>> CreatePrestation([FromBody] AddPrestationDto newPrestation)
        {
            var validator = new AddPrestationDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(newPrestation);

                if (!validationResult.IsValid)
                    throw new ApiException(validationResult.Errors);

                var prestationToCreate = _mapper.Map<AddPrestationDto, Prestation>(newPrestation);
                var newPrestationCreated = await _prestationService.CreatePrestation(prestationToCreate);

                var getPrestationDto = _mapper.Map<Prestation, GetPrestationDto>(newPrestationCreated);

                return Ok(getPrestationDto);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<GetPrestationDto>> UpdatePrestation(int id, [FromBody] UpdatePrestationDto updatePrestationDto)
        {
            var validator = new UpdatePrestationDtoValidator();
            try
            {
                var validationResult = await validator.ValidateAsync(updatePrestationDto);

                if (id == 0 || !validationResult.IsValid)
                    throw new ApiException(validationResult.Errors);

                var prestationToBeUpdate = await _prestationService.GetPrestationById(id);

                if (prestationToBeUpdate == null)
                    return NotFound();

                var prestation = _mapper.Map<UpdatePrestationDto, Prestation>(updatePrestationDto);
                await _prestationService.UpdatePrestation(prestationToBeUpdate, prestation);

                var updatedPrestation = await _prestationService.GetPrestationById(id);

                var updatedPrestationDto = _mapper.Map<Prestation, GetPrestationDto>(updatedPrestation);

                return Ok(updatedPrestation);

            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<GetPrestationDto>>> GetPrestations()
        {
            try
            {
                IEnumerable<Prestation> prestations = await this._prestationService.GetAllPrestations();
                IEnumerable<GetPrestationDto> prestationsDtos = this._mapper.Map<IEnumerable<Prestation>, IEnumerable<GetPrestationDto>>(prestations);

                return Ok(prestationsDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }

        }

        [HttpGet("{id}")]
        public async Task<GetPrestationDto> GetPrestationById(int id)
        {
            try
            {
                var prestation = await this._prestationService.GetPrestationById(id);
                var prestationDto = this._mapper.Map<Prestation, GetPrestationDto>(prestation);

                if (prestationDto == null)
                    throw new ApiException($"La prestation avec pour id : {id} n'existe pas", Status404NotFound);

                return prestationDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePrestation(int id)
        {
            try
            {
                var prestationToBeDeleted = await _prestationService.GetPrestationById(id);

                if(prestationToBeDeleted == null)
                    return NotFound();

                await _prestationService.DeletePrestation(prestationToBeDeleted);

                return new ApiResponse("Prestation has been deleted ", id, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
    }
}