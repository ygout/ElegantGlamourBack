using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core;
using ElegantGlamour.Core.Error;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;
using ElegantGlamour.Core.Specifications;
using ElegantGlamour.Services.Specifications;

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
            try
            {
                var spec = new IsPrestationCategoryExistSpecification(newPrestation.Id.ToString());
                bool isCategoryExist = await _unitOfWork.Prestations.IsPrestationCategoryExistAsync(spec);
                if (!isCategoryExist)
                    throw new CategoryDoesNotExistException();
                await _unitOfWork.Prestations.AddAsync(newPrestation);
                await _unitOfWork.CommitAsync();

                return newPrestation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PrestationCategory> CreatePrestationCategory(PrestationCategory newPrestationCategory)
        {
            try
            {
                
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task DeletePrestation(Prestation prestation)
        {
            try
            {
                _unitOfWork.Prestations.Remove(prestation);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Task DeletePrestationCategory(PrestationCategory prestationCategory)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Prestation>> GetAllPrestations()
        {
            try
            {
                return await _unitOfWork.Prestations.GetPrestationsAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<Prestation>> GetAllPrestations(PrestationSpecParams specParam)
        {
            var spec = new PrestationsWithCategoriesSpecification(specParam);

            return await _unitOfWork.Prestations.ListAsync(spec);
        }

        public async Task<Prestation> GetPrestationById(int id)
        {
            try
            {
                var spec = new PrestationsWithCategoriesSpecification(id);
                return await _unitOfWork.Prestations.GetEntityWithSpec(spec);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PrestationCategory> GetPrestationCategoryById(int id)
        {
            var spec = new PrestationsCategorySpecification(id);
            return await _unitOfWork.Prestations.GetPrestationCategoryAsync(spec);
        }

        public async Task<IReadOnlyList<PrestationCategory>> GettAllPrestationCategories(PrestationCategorySpecParams specParams)
        {
            var spec = new PrestationsCategorySpecification(specParams);
            return await _unitOfWork.Prestations.GetPrestationCategoriesAsync(spec);
        }

        public async Task UpdatePrestation(Prestation prestationToBeUpdate, Prestation prestation)
        {
            try
            {
                prestationToBeUpdate.Description = prestation.Description;
                prestationToBeUpdate.Duration = prestation.Duration;
                prestationToBeUpdate.Price = prestation.Price;
                prestationToBeUpdate.Title = prestation.Title;
                var spec = new IsPrestationCategoryExistSpecification(prestationToBeUpdate.PrestationCategoryId.ToString());

                bool isCategoryExist = await _unitOfWork.Prestations.IsPrestationCategoryExistAsync(spec);
                if (!isCategoryExist)
                    throw new CategoryDoesNotExistException();

                prestationToBeUpdate.PrestationCategory = prestation.PrestationCategory;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<PrestationCategory> UpdatePrestationCategory(PrestationCategory prestationCategoryToBeUpdate, PrestationCategory prestationCategory)
        {
            throw new NotImplementedException();
        }
    }
}