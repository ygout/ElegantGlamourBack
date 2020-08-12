﻿using System;
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
            var apiException = await Assert.ThrowsAsync<ApiProblemDetailsException>(() => controller.GetCategoryById(id));
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
            var addCategory = new AddCategoryDto() {
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
            var addCategory = new AddCategoryDto();
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreateCategory(addCategory));

            dbContext.Dispose();
            #endregion

            #region Assert

            Assert.Equal(400, apiException.StatusCode);

            #endregion
        }
    }
}
