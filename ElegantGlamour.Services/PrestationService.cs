using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;

namespace ElegantGlamour.Services
{
    public class PrestationService : IPrestationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrestationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;

        }
        public async Task<Prestation> CreatePrestation(Prestation newPrestation)
        {
            await _unitOfWork.Prestations.AddAsync(newPrestation);
            await _unitOfWork.CommitAsync();
            
            return newPrestation;
        }

        public async Task DeletePrestation(Prestation prestation)
        {
            _unitOfWork.Prestations.Remove(prestation);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Prestation>> GetAllPrestations()
        {
            return await _unitOfWork.Prestations.GetAllAsync();
        }

        public async Task<Prestation> GetPrestationById(int id)
        {
            return await _unitOfWork.Prestations.GetByIdAsync(id);
        }

        public Task UpdatePrestation(Prestation prestationToBeUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}