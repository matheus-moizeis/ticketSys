using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSys.Domain.Entities;

namespace TicketSys.Infra.Data.EntitiesConfiguration;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Address).HasMaxLength(150);
        builder.Property(u => u.Active).IsRequired().HasDefaultValue(true);
    }
}
