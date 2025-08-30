using Domain.Entities.ServiceDesk;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.ServiceDeskEntityTypeConfig
{
    public class ServiceDeskEntityTypeConfig : IEntityTypeConfiguration<ServiceDesk>
    {
        public void Configure(EntityTypeBuilder<ServiceDesk> builder)
        {
            builder.Property(p => p.Title)
                .IsRequired();
            
            builder.HasQueryFilter(f => EF.Property<bool>(f, "IsRemoved") != true);
        }
    }
}