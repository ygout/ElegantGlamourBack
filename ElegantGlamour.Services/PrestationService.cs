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
                var spec = new IsPrestationCategoryExistSpecification(id: newPrestation.PrestationCategoryId.ToString());
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
                var spec = new IsPrestationCategoryExistSpecification(name: newPrestationCategory.Name);
                bool isCategoryAlreadyExist = await _unitOfWork.Prestations.IsPrestationCategoryExistAsync(spec);
                if (isCategoryAlreadyExist)
                    throw new CategoryAlreadyExistException();
                await _unitOfWork.Prestations.AddPrestationCategoryAsync(newPrestationCategory);
                await _unitOfWork.CommitAsync();

                return newPrestationCategory;

            }
            catch (Exception)
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

        public async Task DeletePrestationCategory(PrestationCategory prestationCategory)
        {
            try
            {
                _unitOfWork.Prestations.DeletePrestationCategory(prestationCategory);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                throw;
            }
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

        public async Task UpdatePrestation(Prestation prestationToBeUpdate, Prestation newPrestation)
        {
            try
            {
                prestationToBeUpdate.Description = newPrestation.Description;
                prestationToBeUpdate.Duration = newPrestation.Duration;
                prestationToBeUpdate.Price = newPrestation.Price;
                prestationToBeUpdate.Title = newPrestation.Title;
                var spec = new IsPrestationCategoryExistSpecification(newPrestation.PrestationCategoryId.ToString());

                bool isCategoryExist = await _unitOfWork.Prestations.IsPrestationCategoryExistAsync(spec);
                if (!isCategoryExist)
                    throw new CategoryDoesNotExistException();

                prestationToBeUpdate.PrestationCategory = newPrestation.PrestationCategory;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PrestationCategory> UpdatePrestationCategory(PrestationCategory prestationCategoryToBeUpdate, PrestationCategory prestationCategory)
        {
            try
            {
                var spec = new IsPrestationCategoryExistSpecification(name: prestationCategoryToBeUpdate.Name.ToString());

                bool isCategoryExist = await _unitOfWork.Prestations.IsPrestationCategoryExistAsync(spec);

                if (!isCategoryExist)
                    throw new CategoryDoesNotExistException();

                prestationCategoryToBeUpdate.Name = prestationCategory.Name;

                await _unitOfWork.CommitAsync();

                return prestationCategoryToBeUpdate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}