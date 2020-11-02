using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

namespace ElegantGlamour.Tests.UnitTests
{
    public static class DbContextMocker
    {
        public static ElegantGlamourDbContext GetElegantGlamourDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<ElegantGlamourDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new ElegantGlamourDbContext(options);
            
            ILoggerFactory loggerFactory = new LoggerFactory();
            // Add entities in memory
            dbContext.Seed(loggerFactory);

            return dbContext;
        }
    }
}
