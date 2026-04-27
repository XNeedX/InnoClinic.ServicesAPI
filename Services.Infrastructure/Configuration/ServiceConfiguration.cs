using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Models;

namespace Services.Infrastructure.Configuration;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Price).HasColumnType("decimal(18,2)");
        builder.Property(s => s.Status).HasConversion<string>().IsRequired();
        builder.Property(s => s.Category).HasConversion<string>().IsRequired();

    }
}
