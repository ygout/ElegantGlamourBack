using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.API.Dtos;
using ElegantGlamour.API.Models;

namespace ElegantGlamour.API.Services
{
    public interface IPrestationService
    {
        Task<ServiceResponse<List<GetPrestationDto>>> GetAllPrestations();
        Task<ServiceResponse<GetPrestationDto>> GetPrestationById(int id);
        Task<ServiceResponse<GetPrestationDto>> CreatePrestation(AddPrestationDto newPrestation);
        Task<ServiceResponse<GetPrestationDto>> UpdatePrestation(UpdatePrestationDto newPrestation);
        Task<ServiceResponse<bool>> DeletePrestation(int id);


    }
}