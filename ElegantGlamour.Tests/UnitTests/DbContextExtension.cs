using System;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data;

namespace ElegantGlamour.Tests.UnitTests
{
    public static class DbContextExtension
    {
        public static void Seed(this ElegantGlamourDbContext dbContext)
        {
            dbContext.Categories.Add(new Category()
            {
                Id = 1,
                Title = "Maquillage",
            });

            dbContext.Categories.Add(new Category()
            {
                Id = 2,
                Title = "Soins",
            });

            dbContext.Categories.Add(new Category()
            {
                Id = 3,
                Title = "Massage",
            });

            dbContext.Prestations.Add(new Prestation()
            {
                Id = 1,
                Title = "test",
                Description = "test",
                Price = 30,
                Duration = 45,
                CategoryId = 1,
            });
        }
    }
}