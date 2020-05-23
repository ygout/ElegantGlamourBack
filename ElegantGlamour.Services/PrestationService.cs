using System;
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
            bool isCategoryExist = await _unitOfWork.Categories.IsCategoryIdExist(newPrestation.CategoryId);
            if(!isCategoryExist)
                throw new Exception("La categorie n'existe pas");
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

        public async Task UpdatePrestation(Prestation prestationToBeUpdate, Prestation prestation)
        {
            prestationToBeUpdate.Description = prestation.Description;
            prestationToBeUpdate.Duration = prestation.Duration;
            prestationToBeUpdate.Price = prestation.Price;
            prestationToBeUpdate.Title = prestation.Title;
            prestationToBeUpdate.Category = prestation.Category;

            await _unitOfWork.CommitAsync();
        }
    }
}