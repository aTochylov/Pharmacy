using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Models;

namespace Pharmacy.Data.Configurations
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(m => m.MedicineId);
            builder.Property(m => m.Barcode).IsRequired();
            builder.HasIndex(m => m.Barcode).IsUnique();
            builder.Property(m => m.Title).IsRequired();
            
        }
    }
}
