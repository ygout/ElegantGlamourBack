using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;

namespace ElegantGlamour.Core.Services
{
    public interface IPrestationService
    {
        Task<IEnumerable<Prestation>> GetAllPrestations();
        Task<Prestation> GetPrestationById(int id);
        Task<Prestation> CreatePrestation(Prestation newPrestation);
        Task UpdatePrestation(Prestation prestationToBeUpdate, Prestation prestation);
        Task DeletePrestation(Prestation prestation);
    }
}