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

namespace ElegantGlamour.Tests.UnitTests
{
    public class PrestationsController_Test
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

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();
            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);

            return new InitTest() { dbContext = dbContext, controller = controller };
        }
        /// <summary>
        /// Test the Get a prestation by an id and return OK
        /// </summary>
        [Fact]
        public async Task GetPrestationById_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

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
        public async Task GetPrestationById_Return_Not_Found()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestationById_Return_Not_Found));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

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
        public async void GetPrestations_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(GetPrestations_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion

            #region Act
            var specParams = new PrestationSpecParams();
            var response = await controller.GetPrestations(specParams);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsAssignableFrom<IEnumerable<GetPrestationDto>>(response);
            #endregion
        }

        /// <summary>
        /// Create a prestation response OK 201
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "TEST_PRESTATION",
                Description = "Ceci est un test prestation",
                Price = 10,
                Duration = 60,
                PrestationCategoryId = 1
            };
            #region Act
            var response = await controller.CreatePrestation(addPrestation);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(201, response.StatusCode);
            #endregion
        }

        /// <summary>
        /// Create a prestation response OK 201
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Category_Does_Not_Exist()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Category_Does_Not_Exist));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "TEST_PRESTATION",
                Description = "Ceci est un test prestation",
                Price = 10,
                Duration = 60,
                PrestationCategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Does_Not_Exist, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error title can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Title_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Title_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "",
                Description = "Test",
                Price = 10,
                Duration = 60,
                PrestationCategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Title_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Create a prestation with error description can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Description_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Description_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "",
                Price = 10,
                Duration = 60,
                PrestationCategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Description_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error price can't be at null
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Price_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Price_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Duration = 60,
                PrestationCategoryId = 89
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Price_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error duration can't be equal to 0
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Duration_Equal_Zero()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Duration_Equal_Zero));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Duration = 0,
                PrestationCategoryId = 89,
                Price = 50
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Duration_Not_Equal_To_0, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Create a prestation with error duration can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Duration_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Duration_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                PrestationCategoryId = 89,
                Price = 50
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Duration_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Create a prestation with error category id can't be empty
        /// </summary>
        [Fact]
        public async Task POST_Create_Prestation_Return_Error_Category_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(POST_Create_Prestation_Return_Error_Category_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var addPrestation = new AddPrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Price = 50,
                Duration = 50
            };
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.CreatePrestation(addPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Update a prestation with error category id can't be empty
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Category_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Category_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Title = "Test",
                Description = "Test",
                Price = 50,
                Duration = 50
            };
            int idPrestation = 1;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Update a prestation with error Description can't be empty
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Description_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Description_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Title = "Test",
                Price = 50,
                Duration = 50,
                PrestationCategoryId = 1
            };
            int idPrestation = 1;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Description_Not_Empty, apiException.CustomError.ToString());

            #endregion

        }

        /// <summary>
        /// Update a prestation with error Title can't be empty
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Title_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Title_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Price = 50,
                Description = "this is description",
                Duration = 50,
                PrestationCategoryId = 1
            };
            int idPrestation = 1;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Title_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Update a prestation with error Price can't be empty
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Price_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Price_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Description = "this is description",
                Duration = 50,
                Title = "test title",
                PrestationCategoryId = 1
            };
            int idPrestation = 1;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Price_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }
        /// <summary>
        /// Update a prestation with error Duration can't be empty
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Duration_Empty()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Duration_Empty));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Description = "this is description",
                Price = 50,
                Title = "test title",
                PrestationCategoryId = 1
            };
            int idPrestation = 1;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Duration_Not_Empty, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Update a prestation with error category id does not exist
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Category_Does_Not_Exist()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Category_Does_Not_Exist));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Description = "this is description",
                Price = 50,
                Title = "test title",
                Duration = 60,
                PrestationCategoryId = 8544
            };
            int idPrestation = 1;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(400, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Category_Does_Not_Exist, apiException.CustomError.ToString());

            #endregion
        }

        /// <summary>
        /// Update a prestation with error prestation id does not exist
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Error_Prestation_Id_Does_Not_Exist()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Error_Prestation_Id_Does_Not_Exist));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Description = "this is description",
                Price = 50,
                Title = "test title",
                Duration = 60,
                PrestationCategoryId = 1
            };
            int idPrestation = 8787887;
            #region Act

            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.UpdatePrestation(idPrestation, updatePrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Id_Does_Not_Exist, apiException.Message);

            #endregion
        }

        /// <summary>
        /// Update a prestation with no error is OK
        /// </summary>
        [Fact]
        public async Task Put_Update_Prestation_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Put_Update_Prestation_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion
            var updatePrestation = new UpdatePrestationDto()
            {
                Description = "this is description updated",
                Price = 60,
                Title = "test title updated",
                Duration = 120,
                PrestationCategoryId = 1
            };
            int idPrestation = 1;

            #region Act
            var response = await controller.UpdatePrestation(idPrestation, updatePrestation);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(201, response.StatusCode);
            #endregion
        }

        /// <summary>
        /// Delete a prestation return OK
        /// </summary>
        [Fact]
        public async Task Delete_Prestation_Return_Ok()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Delete_Prestation_Return_Ok));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion

            int idPrestation = 1;

            #region Act
            var response = await controller.DeletePrestation(idPrestation);

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.IsType<ApiResponse>(response);
            Assert.Equal(200, response.StatusCode);
            #endregion
        }

        /// <summary>
        /// Delete a prestation return OK
        /// </summary>
        [Fact]
        public async Task Delete_Prestation_Error_Prestation_Id_Does_Not_Exist()
        {
            #region Arrange
            var dbContext = DbContextMocker.GetElegantGlamourDbContext(nameof(Delete_Prestation_Error_Prestation_Id_Does_Not_Exist));
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = new Mapper(config);

            var mockUnitOfWork = new UnitOfWork(dbContext);
            var mockPrestationService = new PrestationService(mockUnitOfWork);

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
            #endregion

            int idPrestation = 1500;

            #region Act
            var apiException = await Assert.ThrowsAsync<ApiException>(() => controller.DeletePrestation(idPrestation));

            dbContext.Dispose();
            #endregion

            #region Assert
            Assert.Equal(404, apiException.StatusCode);
            Assert.Contains(ErrorMessage.Err_Prestation_Id_Does_Not_Exist, apiException.Message);

            #endregion
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

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
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

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
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

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockPrestationService, mapper, mockLogger);
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

            var mockLogger = Mock.Of<ILogger<PrestationsController>>();

            var controller = new PrestationsController(mockCateogryService, mapper, mockLogger);
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
            var controller = (PrestationsController)initTest.controller;
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
            var controller = (PrestationsController)initTest.controller;
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
            var controller = (PrestationsController)initTest.controller;
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
            var controller = (PrestationsController)initTest.controller;
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
            var controller = (PrestationsController)initTest.controller;
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
            var controller = (PrestationsController)initTest.controller;
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
            var controller = (PrestationsController)initTest.controller;
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