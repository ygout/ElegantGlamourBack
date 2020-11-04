using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ElegantGlamour.Core.Models;
using Microsoft.Extensions.Logging;

namespace ElegantGlamour.Data
{
    public static class ElegantGlamourContextSeed
    {
        public static void Seed(this ElegantGlamourDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var rootPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));

                if (!context.PrestationCategories.Any())
                {

                    var categoriesData = File.ReadAllText(rootPath + "/UnitTests/SeedData/categories.json");

                    var categories = JsonSerializer.Deserialize<List<PrestationCategory>>(categoriesData);

                    foreach (var item in categories)
                    {
                        context.PrestationCategories.Add(item);
                    }

                    context.SaveChanges();
                }

                if (!context.Prestations.Any())
                {
                    var prestationsData = File.ReadAllText(rootPath + "/UnitTests/SeedData/prestations.json");

                    var prestations = JsonSerializer.Deserialize<List<Prestation>>(prestationsData);

                    foreach (var item in prestations)
                    {
                        context.Prestations.Add(item);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ElegantGlamourDbContext>();
                logger.LogError(ex.Message);
            }
        }
        public static async Task SeedAsync(this ElegantGlamourDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var rootPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));

                if (!context.PrestationCategories.Any())
                {

                    var categoriesData = File.ReadAllText("../ElegantGlamour.Data/SeedData/categories.json");

                    var categories = JsonSerializer.Deserialize<List<PrestationCategory>>(categoriesData);

                    foreach (var item in categories)
                    {
                        context.PrestationCategories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Prestations.Any())
                {
                    var prestationsData = File.ReadAllText("../ElegantGlamour.Data/SeedData/prestations.json");

                    var prestations = JsonSerializer.Deserialize<List<Prestation>>(prestationsData);

                    foreach (var item in prestations)
                    {
                        context.Prestations.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ElegantGlamourDbContext>();
                logger.LogError(ex.Message);
            }
        }

    }
}