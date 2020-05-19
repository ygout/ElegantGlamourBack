using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.Api.Validators;
using ElegantGlamour.Core.Dtos;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
                _logger.LogCritical("There was an error on '{0}' invocation: {1}", MethodBase.GetCurrentMethod(), ex);
                throw;
            }
        }
    }
}