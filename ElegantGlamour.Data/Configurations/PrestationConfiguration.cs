using ElegantGlamour.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElegantGlamour.Data.Configurations
{
    public class PrestationConfiguration : IEntityTypeConfiguration<Prestation>
    {
        public void Configure(EntityTypeBuilder<Prestation> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).UseMySqlIdentityColumn();

            builder.Property(p => p.Title).IsRequired().HasMaxLength(50);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Prestations)
                .HasForeignKey(p => p.CategoryId);
                
            builder.ToTable("Prestations");

        }
    }
}