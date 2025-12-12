using Domain.Entities.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.StoreConfig;

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.PostalCode)
            .HasMaxLength(11);
        
        builder.Property(x => x.OrderNumber)
            .HasMaxLength(50);
    }
}