using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ElegantGlamour.API.Data;
using ElegantGlamour.API.Dtos;
using ElegantGlamour.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElegantGlamour.API.Services
{
    public class PrestationService : IPrestationService
    {
        private readonly DataContext _context;
        private readonly IMapper _autoMapper;
        public PrestationService(DataContext context, IMapper autoMapper)
        {
            this._autoMapper = autoMapper;
            this._context = context;

        }
        public async Task<ServiceResponse<GetPrestationDto>> CreatePrestation(AddPrestationDto newPrestation)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<bool>> DeletePrestation(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetPrestationDto>>> GetAllPrestations()
        {
            var serviceResponse = new ServiceResponse<List<GetPrestationDto>>();
            serviceResponse.Data = await this._context.Prestations.Select( p => this._autoMapper.Map<GetPrestationDto>(p)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPrestationDto>> GetPrestationById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPrestationDto>();

            var prestation = await this._context.Prestations.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = this._autoMapper.Map<GetPrestationDto>(prestation);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPrestationDto>> UpdatePrestation(UpdatePrestationDto newPrestation)
        {
            throw new System.NotImplementedException();
        }
    }
}