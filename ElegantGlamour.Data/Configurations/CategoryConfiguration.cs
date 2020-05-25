using ElegantGlamour.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElegantGlamour.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).UseMySqlIdentityColumn();

            builder.Property(c => c.Title).IsRequired().HasMaxLength(50);
            
            builder.ToTable("Categories");
        }
    }
}