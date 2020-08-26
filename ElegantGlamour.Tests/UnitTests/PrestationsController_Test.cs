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
using ElegantGlamour.API.Controllers;

namespace ElegantGlamour.Tests.UnitTests
{
    public class PrestationsController_Test
    {
        /// <summary>
        /// Test the Get a prestation by an id and return OK
        /// </summary>
        [Fact]
        public async Task GetPrestationById_RETURN_OK()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_RETURN_OK));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCategoryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCategoryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            var id = 1;
            #endregion

            #region Act
            var response = await controller.GetPrestationById(id);
            dbContext.Dispose();

            #endregion

            #region Assert
            Assert.IsType<GetPrestationDto>(response);
            #endregion
        }

        /// <summary>
        /// Test the Get a prestation by an id and return NOT_FOUND
        /// </summary>
        [Fact]
        public async Task GetPrestationById_RETURN_NOT_FOUND()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_RETURN_NOT_FOUND));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCategoryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCategoryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            var id = 300;
            #endregion
            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.GetPrestationById(id));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            #endregion

        }

        /// <summary>
        /// Test return all prestations response 200 OK
        /// </summary>
        [Fact]
        public async void GetPrestations_RETURN()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestations_RETURN));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCategoryService = new CategoryService(mockUnitOfWork);
            var mockPrestationService = new PrestationService(mockUnitOfWork, mockCategoryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion

            #region Act
            var response = await controller.GetPrestations();

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsAssignableFrom<IEnumerable<GetPrestationDto>>(response);
            #endregion
        }
    }
}
