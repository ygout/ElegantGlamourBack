using Microsoft.EntityFrameworkCore;
using ElegantGlamour.Core.Models;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ElegantGlamour.Core.Models.Entity.Auth;
using System;

namespace ElegantGlamour.Data
{
    public class ElegantGlamourDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<PrestationCategory> PrestationCategories { get; set; }
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
        }
    }
}