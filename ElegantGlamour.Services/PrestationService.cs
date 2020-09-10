using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElegantGlamour.Core;
using ElegantGlamour.Core.Error;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Services;

namespace ElegantGlamour.Services
{
    public class PrestationService : IPrestationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _categoryService;
        public PrestationService(IUnitOfWork unitOfWork, ICategoryService categoryService)
        {
            this._categoryService = categoryService;
            this._unitOfWork = unitOfWork;

        }

        public async Task<Prestation> CreatePrestation(Prestation newPrestation)
        {
            try
            {
                bool isCategoryExist = await _categoryService.IsCategoryIdExist(newPrestation.CategoryId);
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

        public async Task<IEnumerable<Prestation>> GetAllPrestations()
        {
            try
            {
                return await _unitOfWork.Prestations.GetAllPrestationsWithCategoryAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<Prestation> GetPrestationById(int id)
        {
            try
            {
                return await _unitOfWork.Prestations.GetPrestationWithCategoryByIdAsync(id);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePrestation(Prestation prestationToBeUpdate, Prestation prestation)
        {
            try
            {
                prestationToBeUpdate.Description = prestation.Description;
                prestationToBeUpdate.Duration = prestation.Duration;
                prestationToBeUpdate.Price = prestation.Price;
                prestationToBeUpdate.Title = prestation.Title;

                bool isCategoryExist = await _categoryService.IsCategoryIdExist(prestation.CategoryId);
                if (!isCategoryExist)
                    throw new Exception("La categorie n'existe pas");

                prestationToBeUpdate.Category = prestation.Category;

                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}