using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data.Configurations;

namespace ElegantGlamour.Data
{
    public class ElegantGlamourDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Prestation> Prestations { get; set; }
        public ElegantGlamourDbContext(DbContextOptions<ElegantGlamourDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category() { Id = 1, Title = "Maquillage" },
                new Category() { Id = 2, Title = "Soins" },
                new Category() { Id = 3, Title = "Massage" }
            );

            builder.Entity<Prestation>().HasData(
                new Prestation()
                {
                    Id = 1,
                    Title = "Prestation1",
                    Description = "ceci est la préstation numéro 1",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 1,
                },
                new Prestation()
                {
                    Id = 2,
                    Title = "Prestation2",
                    Description = "ceci est la préstation numéro 2",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 2,
                },
                new Prestation()
                {
                    Id = 3,
                    Title = "Prestation3",
                    Description = "ceci est la préstation numéro 3",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 3,
                },
               new Prestation()
               {
                   Id = 4,
                   Title = "Prestation1",
                   Description = "ceci est la préstation numéro 4",
                   Price = 30,
                   Duration = 45,
                   CategoryId = 1,
               },
                new Prestation()
                {
                    Id = 5,
                    Title = "Prestation1",
                    Description = "ceci est la préstation numéro 5",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 1,
                },
                new Prestation()
                {
                    Id = 6,
                    Title = "Prestation1",
                    Description = "ceci est la préstation numéro 6",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 2,
                },
                new Prestation()
                {
                    Id = 7,
                    Title = "Prestation1",
                    Description = "ceci est la préstation numéro 7",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 2,
                },
                new Prestation()
                {
                    Id = 8,
                    Title = "Prestation1",
                    Description = "ceci est la préstation numéro 8",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 3,
                }
            );
        }
    }
}