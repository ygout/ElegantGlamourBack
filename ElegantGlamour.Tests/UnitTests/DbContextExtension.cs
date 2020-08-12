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
                Title = "Prestation1",
                Description = "ceci est la préstation numéro 1",
                Price = 30,
                Duration = 45,
                CategoryId = 1,
            });

            dbContext.Prestations.Add(new Prestation()
            {
                Id = 2,
                Title = "Prestation2",
                Description = "ceci est la préstation numéro 2",
                Price = 30,
                Duration = 45,
                CategoryId = 2,
            });

            dbContext.Prestations.Add(new Prestation()
            {
                Id = 3,
                Title = "Prestation3",
                Description = "ceci est la préstation numéro 3",
                Price = 30,
                Duration = 45,
                CategoryId = 3,
            });

            dbContext.Prestations.Add(new Prestation()
            {
                Id = 4,
                Title = "Prestation1",
                Description = "ceci est la préstation numéro 4",
                Price = 30,
                Duration = 45,
                CategoryId = 1,
            });
            dbContext.Prestations.Add(new Prestation()
            {
                Id = 5,
                Title = "Prestation1",
                Description = "ceci est la préstation numéro 5",
                Price = 30,
                Duration = 45,
                CategoryId = 1,
            });
            dbContext.Prestations.Add(new Prestation()
            {
                Id = 6,
                Title = "Prestation1",
                Description = "ceci est la préstation numéro 6",
                Price = 30,
                Duration = 45,
                CategoryId = 2,
            });
            dbContext.Prestations.Add(new Prestation()
            {
                Id = 7,
                Title = "Prestation1",
                Description = "ceci est la préstation numéro 7",
                Price = 30,
                Duration = 45,
                CategoryId = 2,
            });
            dbContext.Prestations.Add(new Prestation()
            {
                Id = 8,
                Title = "Prestation1",
                Description = "ceci est la préstation numéro 8",
                Price = 30,
                Duration = 45,
                CategoryId = 3,
            });

            dbContext.SaveChanges();
        }
    }
}