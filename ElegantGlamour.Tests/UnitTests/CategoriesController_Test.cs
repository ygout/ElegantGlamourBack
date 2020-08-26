using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data;
using ElegantGlamour.Api.Controllers;
using Xunit;
using ElegantGlamour.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoWrapper.Wrappers;
using ElegantGlamour.Api.Mapping;
using ElegantGlamour.Core.Dtos;
using System.Threading.Tasks;
using ElegantGlamour.Core.Error;

namespace ElegantGlamour.Tests.UnitTests
{
    public class CategoriesController_Test
    {
        /// <summary>
        /// Test the GetCategory method
        /// </summary>
        [Fact]
        public async Task GetCategoryById_RETURNS_OK()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetCategoryById_RETURNS_OK));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            var id = 1;
            #endregion

            #region Act
            var response = await controller.GetCategoryById(id);
            dbContext.Dispose();

            #endregion

            #region Assert
            Assert.IsType<GetCategoryDto>(response);
            #endregion
        }


        /// <summary>
        /// Test the GetCategory method not found response
        /// </summary>
        [Fact]
        public async Task GetCategoryById_RETURNS_NOT_FOUND()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetCategoryById_RETURNS_NOT_FOUND));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            var id = 0;
            #endregion

            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.GetCategoryById(id));
            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion
        }
        /// <summary>
        /// Test the GetCategories method response OK
        /// </summary>
        [Fact]
        public async void GetCategories_RETURN()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetCategories_RETURN));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion

            #region Act
            var response = await controller.GetCategories();

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsAssignableFrom<IEnumerable<GetCategoryDto>>(response);
            #endregion
        }

        /// <summary>
        /// Test the CreateCategory method response OK
        /// </summary>
        [Fact]
        public async Task POST_Create_RETURNS_OK()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_RETURNS_OK));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var addCategory = new AddCategoryDto()
            {
                Title = "TEST_CATEGORY",
            };
            #region Act
            var response = await controller.CreateCategory(addCategory);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(201, response.StatusCode);
            #endregion
        }
        /// <summary>
        /// Test the CreateCategory method response Erreur not empty
        /// </summary>
        [Fact]
        public async Task POST_Create_RETURN_ERROR_NOT_EMPTY()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_RETURN_ERROR_NOT_EMPTY));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var addCategory = new AddCategoryDto();
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreateCategory(addCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Not_Empty, apiException.CustomError.ToString());
            #endregion
        }
        /// <summary>
        /// Test the CreateCategory method response Erreur 50 length Max
        /// </summary>
        [Fact]
        public async Task POST_Create_RETURN_ERROR_MAX_LENGTH()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_RETURN_ERROR_MAX_LENGTH));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var addCategory = new AddCategoryDto() { Title = "TitreDuneCategorieAvecPlusde50caractereouicestbeacoutroplongjenesaistoujourspasquoimettreestonaplusde50caractere???" };
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreateCategory(addCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Max_Size, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the CreateCategory method response Erreur 50 length Max
        /// </summary>
        [Fact]
        public async Task POST_Create_RETURN_ERROR_ALREADY_EXIST()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_RETURN_ERROR_ALREADY_EXIST));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var addCategory = new AddCategoryDto() { Title = "Maquillage" };
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreateCategory(addCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Already_Exist, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the UpdateCategory not found
        /// </summary>
        [Fact]
        public async Task PUT_UPDATE_CATEGORY_ERROR_NOT_FOUND()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(PUT_UPDATE_CATEGORY_ERROR_NOT_FOUND));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            int idCategory = 50;
            var updateCategory = new UpdateCategoryDto() { Title = "Maquillage modifié" };
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdateCategory(idCategory, updateCategory));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion
        }

        /// <summary>
        /// Test the UpdateCategory method response Erreur not empty
        /// </summary>
        [Fact]
        public async Task PUT_UPDATE_CATEGORY_RETURN_ERROR_NOT_EMPTY()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(PUT_UPDATE_CATEGORY_RETURN_ERROR_NOT_EMPTY));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var updateCategory = new UpdateCategoryDto();
            int idCategory = 1;
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdateCategory(idCategory, updateCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Not_Empty, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the UpdateCategory method response Erreur 50 length Max
        /// </summary>
        [Fact]
        public async Task PUT_UPDATE_CATEGORY_RETURN_ERROR_MAX_LENGTH()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(PUT_UPDATE_CATEGORY_RETURN_ERROR_MAX_LENGTH));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var updateCategory = new UpdateCategoryDto() { Title = "TitreDuneCategorieAvecPlusde50caractereouicestbeacoutroplongjenesaistoujourspasquoimettreestonaplusde50caractere???" };
            int idCategory = 1;
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdateCategory(idCategory, updateCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Max_Size, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the UpdateCategory method response Erreur 50 length Max
        /// </summary>
        [Fact]
        public async Task PUT_UPDATE_CATEGORY_RETURN_ERROR_ALREADY_EXIST()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(PUT_UPDATE_CATEGORY_RETURN_ERROR_ALREADY_EXIST));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var updateCategory = new UpdateCategoryDto() { Title = "Maquillage" };
            int idCategory = 1;
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdateCategory(idCategory, updateCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Already_Exist, apiException.CustomError.ToString());
            #endregion
        }

        /// <summary>
        /// Test the Update Category method response OK
        /// </summary>
        [Fact]
        public async Task PUT_UPDATE_CATEGORY_RETURNS_OK()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(PUT_UPDATE_CATEGORY_RETURNS_OK));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            var updateCategory = new UpdateCategoryDto()
            {
                Title = "TEST_CATEGORY_UPDATE",
            };
            int idCategory = 1;
            #region Act
            var response = await controller.UpdateCategory(idCategory, updateCategory);

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
        public async Task DELETE_CATEGORY_RETURNS_OK()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(DELETE_CATEGORY_RETURNS_OK));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            int idCategory = 1;
            #region Act
            var response = await controller.DeleteCategory(idCategory);

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
        public async Task DELETE_CATEGORY_RETURNS_NOT_FOUND()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(DELETE_CATEGORY_RETURNS_NOT_FOUND));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<CategoriesController>>();

            var controller = new CategoriesController(mockCateogryService, mapper, mockLogger);
            #endregion
            int idCategory = 75;
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.DeleteCategory(idCategory));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion
        }

    }
}
