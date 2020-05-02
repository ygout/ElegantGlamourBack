using ElegantGlamour.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ElegantGlamour.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Prestation> Prestations { get; set; }

    }
}