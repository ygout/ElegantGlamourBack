using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data;
using Xunit;
using ElegantGlamour.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using ElegantGlamour.Api.Mapping;
using ElegantGlamour.Api.Dtos;
using System.Threading.Tasks;
using ElegantGlamour.Core.Error;
using ElegantGlamour.API.Controllers;
using ElegantGlamour.Services.Specifications;
using ElegantGlamour.Core.Specifications;
using ElegantGlamour.Api.Controllers;

namespace ElegantGlamour.Tests.UnitTests
{
    public class PrestationCategoriesController_Test
    {
         private InitTest InitPrestationTest(string dbtestName)
        {
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(dbtestName);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationCategoriesController>>();
            var controller = new PrestationCategoriesController(mockPrestationService, mapper, mockLogger);

            return new InitTest() { dbContext = dbContext, controller = controller };
        }

        /// <summary>
        /// Recover a prestation category by Id
        /// </summary>
        [Fact]
        public async Task GetPrestationCategoryById_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationCategoryById_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationCategoriesController>>();

            var controller = new PrestationCategoriesController(mockPrestationService, mapper, mockLogger);
            var id = 1;
            #endregion

            #region Act
            var response = await controller.GetPrestationCategoryById(id);
            dbContext.Dispose();

            #endregion

            #region Assert
            Assert.IsType<GetPrestationCategoryDto>(response);
            #endregion
        }

        /// <summary>
        /// Test the GetPrestationCategory method not found response
        /// </summary>
        [Fact]
        public async Task GetPrestationCategoryById_Return_Not_Found()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationCategoryById_Return_Not_Found));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationCategoriesController>>();

            var controller = new PrestationCategoriesController(mockPrestationService, mapper, mockLogger);
            var id = 0;
            #endregion

            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.GetPrestationCategoryById(id));
            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion
        }

        /// <summary>
        /// Test the GetPrestationCategories method response OK
        /// </summary>
        [Fact]
        public async void GetPrestationCategories_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationCategories_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationCategoriesController>>();

            var controller = new PrestationCategoriesController(mockPrestationService, mapper, mockLogger);
            #endregion

            #region Act
            PrestationCategorySpecParams spec = new PrestationCategorySpecParams();
            var response = await controller.GetPrestationCategories(spec);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsAssignableFrom<IEnumerable<GetPrestationCategoryDto>>(response);
            #endregion
        }

        /// <summary>
        /// Test the CreatePrestationCategory method response OK
        /// </summary>
        [Fact]
        public async Task POST_CreatePrestationCategory_Return_ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_CreatePrestationCategory_Return_ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationCategoriesController>>();

            var controller = new PrestationCategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var addCategory = new AddPrestationCategoryDto()
            {
                Name = "TEST_CATEGORY",
            };
            #region Act
            var response = await controller.CreatePrestationCategory(addCategory);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(201, response.StatusCode);
            #endregion
        }

        /// <summary>
        /// Test the CreateCategory method response Erreur title not empty
        /// </summary>
        [Fact]
        public async Task POST_CreatePrestationCategory_Return_Error_Title_Not_Empty()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(POST_CreatePrestationCategory_Return_Error_Title_Not_Empty));
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            var addCategory = new AddPrestationCategoryDto();
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestationCategory(addCategory));

            initTest.dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Title_Not_Empty, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the CreateCategory method response Erreur title exceeds  50 length Max
        /// </summary>
        [Fact]
        public async Task POST_CreatePrestationCategory_Return_Error_Title_Max_Length_Exceeds()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(POST_CreatePrestationCategory_Return_Error_Title_Max_Length_Exceeds));
            var dbContext = initTest.dbContext;
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            var addCategory = new AddPrestationCategoryDto() { Name = "TitreDuneCategorieAvecPlusde50caractereouicestbeacoutroplongjenesaistoujourspasquoimettreestonaplusde50caractere???" };
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestationCategory(addCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Title_Max_Size, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the CreateCategory method response Erreur 50 length Max
        /// </summary>
        [Fact]
        public async Task POST_CreatePrestationCategory_Return_Error_Title_Already_Exist()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(POST_CreatePrestationCategory_Return_Error_Title_Already_Exist));
            var dbContext = initTest.dbContext;
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            var addCategory = new AddPrestationCategoryDto() { Name = "Maquillage" };
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestationCategory(addCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Already_Exist, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the UpdateCategory not found id not found
        /// </summary>
        [Fact]
        public async Task Put_Update_PrestationCategory_Error_Not_Found()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(Put_Update_PrestationCategory_Error_Not_Found));
            var dbContext = initTest.dbContext;
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            int idCategory = 50;
            var updateCategory = new UpdatePrestationCategoryDto() { Name = "Maquillage modifié" };
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestationCategory(idCategory, updateCategory));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion
        }
        /// <summary>
        /// Test the Update Category method response OK
        /// </summary>
        [Fact]
        public async Task Put_Update_PrestationCategory_Return_Ok()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(Put_Update_PrestationCategory_Return_Ok));
            var dbContext = initTest.dbContext;
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            var updateCategory = new UpdatePrestationCategoryDto()
            {
                Name = "TEST_CATEGORY_UPDATE",
            };
            int idCategory = 1;
            #region Act
            var response = await controller.UpdatePrestationCategory(idCategory, updateCategory);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(201, response.StatusCode);
            #endregion
        }
        /// <summary>
        /// Test the delete Category method response OK
        /// </summary>
        [Fact]
        public async Task Delete_PrestationCategory_Return_Ok()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(Delete_PrestationCategory_Return_Ok));
            var dbContext = initTest.dbContext;
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            int idCategory = 1;
            #region Act
            var response = await controller.DeletePrestationCategory(idCategory);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(200, response.StatusCode);
            #endregion
        }
        /// <summary>
        /// Test the delete Category method with unknown id return not found
        /// </summary>
        [Fact]
        public async Task Delete_PrestationCategory_Return_Not_Found()
        {
            #region Arrange
            var initTest = InitPrestationTest(nameof(Delete_PrestationCategory_Return_Not_Found));
            var dbContext = initTest.dbContext;
            var controller = (PrestationCategoriesController)initTest.controller;
            #endregion
            int idCategory = 75;
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.DeletePrestationCategory(idCategory));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion
        }
    }
}