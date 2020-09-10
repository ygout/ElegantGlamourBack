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
        /// Test the GetCategory method
        /// </summary>
        [Fact]
        public async Task GetPrestationById_RETURNS_OK()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_RETURNS_OK));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockCateogryService = new CategoryService(mockUnitOfWork);
            var mockprestationService = new PrestationService(mockUnitOfWork, mockCateogryService);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockprestationService, mapper, mockLogger);
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
    }
}
