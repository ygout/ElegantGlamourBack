using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Data.Configurations;
using System.Reflection;
using System.Linq;

namespace ElegantGlamour.Data
{
    public class ElegantGlamourDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Prestation> Prestations { get; set; }
        public ElegantGlamourDbContext(DbContextOptions<ElegantGlamourDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    foreach (var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }
                }
            }
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
                    Description = "ceci est la pr�station num�ro 1",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 1,
                },
                new Prestation()
                {
                    Id = 2,
                    Title = "Prestation2",
                    Description = "ceci est la pr�station num�ro 2",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 2,
                },
                new Prestation()
                {
                    Id = 3,
                    Title = "Prestation3",
                    Description = "ceci est la pr�station num�ro 3",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 3,
                },
               new Prestation()
               {
                   Id = 4,
                   Title = "Prestation1",
                   Description = "ceci est la pr�station num�ro 4",
                   Price = 30,
                   Duration = 45,
                   CategoryId = 1,
               },
                new Prestation()
                {
                    Id = 5,
                    Title = "Prestation1",
                    Description = "ceci est la pr�station num�ro 5",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 1,
                },
                new Prestation()
                {
                    Id = 6,
                    Title = "Prestation1",
                    Description = "ceci est la pr�station num�ro 6",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 2,
                },
                new Prestation()
                {
                    Id = 7,
                    Title = "Prestation1",
                    Description = "ceci est la pr�station num�ro 7",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 2,
                },
                new Prestation()
                {
                    Id = 8,
                    Title = "Prestation1",
                    Description = "ceci est la pr�station num�ro 8",
                    Price = 30,
                    Duration = 45,
                    CategoryId = 3,
                }
            );
        }
    }
}