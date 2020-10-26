using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Specifications;

namespace ElegantGlamour.Core.Services
{
    public interface IPrestationService
    {
        Task<IEnumerable<Prestation>> GetAllPrestations(PrestationSpecParams specParam);
        Task<Prestation> GetPrestationById(int id);
        Task<Prestation> CreatePrestation(Prestation newPrestation);
        Task UpdatePrestation(Prestation prestationToBeUpdate, Prestation prestation);
        Task DeletePrestation(Prestation prestation);

        Task<PrestationCategory> CreatePrestationCategory(PrestationCategory newPrestationCategory);
        Task<PrestationCategory> UpdatePrestationCategory(PrestationCategory prestationCategoryToBeUpdate, PrestationCategory prestationCategory);
        Task DeletePrestationCategory(PrestationCategory prestationCategory);

        Task<IReadOnlyList<PrestationCategory>> GettAllPrestationCategories();
        Task<PrestationCategory> GetPrestationCategoryById(int id);

    }
}