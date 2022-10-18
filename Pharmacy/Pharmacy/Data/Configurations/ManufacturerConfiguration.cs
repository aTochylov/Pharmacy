using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Models;

namespace Pharmacy.Data.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(m => m.ManufacturerId);
            builder.Property(m => m.Title).IsRequired();

            builder.Property(m => m.Phone).IsRequired();
            builder.HasIndex(m => m.Phone).IsUnique();

            builder.Property(m => m.Address).IsRequired();
            builder.HasMany(m => m.Medicines);
        }
    }
}
