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
        }

    }
}