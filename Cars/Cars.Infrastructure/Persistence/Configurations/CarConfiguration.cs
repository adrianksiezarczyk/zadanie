using Cars.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cars.Infrastructure.Persistence.Configurations;

public class WholesalerProductConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(c => c.Brand).HasMaxLength(50);
        builder.Property(c => c.Model).HasMaxLength(100);
    }
}