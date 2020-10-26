using ElegantGlamour.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElegantGlamour.Data.Configurations
{
    public class PrestationCategoryConfiguration : IEntityTypeConfiguration<PrestationCategory>
    {
        public void Configure(EntityTypeBuilder<PrestationCategory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseMySqlIdentityColumn().IsRequired();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            
        }
    }
}