using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElegantGlamour.API.Data;
using ElegantGlamour.API.Models;
using ElegantGlamour.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ElegantGlamour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestationsController : ControllerBase
    {
        private readonly IPrestationService _prestationService;

        public PrestationsController(IPrestationService prestationService)
        {
            this._prestationService = prestationService;

        }
        [HttpGet]
        public async Task<IActionResult> GetPrestations()
        {
            var prestations = await this._prestationService.GetAllPrestations();
            return Ok(prestations);
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> GetPrestationById(int id)
        {
            var prestation = await this._prestationService.GetPrestationById(id);
            return Ok(prestation);
        }
    }
}