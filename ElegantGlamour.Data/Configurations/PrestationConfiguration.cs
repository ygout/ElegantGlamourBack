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

            builder.Property(p => p.Id).UseMySqlIdentityColumn().IsRequired();

            builder.Property(p => p.Title).IsRequired().HasMaxLength(50);

            builder
                .HasOne(p => p.PrestationCategory)
                .WithMany()
                .HasForeignKey(p => p.PrestationCategoryId);
                
        }
    }
}