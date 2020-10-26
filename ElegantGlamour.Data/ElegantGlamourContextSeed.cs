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
    public class ElegantGlamourContextSeed
    {
        public static async Task SeedAsync(ElegantGlamourDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.PrestationCategories.Any())
                {
                    var categoriesData = File.ReadAllText("./SeedData/categories.json");

                    var categories = JsonSerializer.Deserialize<List<PrestationCategory>>(categoriesData);

                    foreach(var item in categories)
                    {
                        context.PrestationCategories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Prestations.Any())
                {
                    var prestationsData = File.ReadAllText("./SeedData/prestations.json");

                    var prestations = JsonSerializer.Deserialize<List<Prestation>>(prestationsData);

                    foreach(var item in prestations)
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