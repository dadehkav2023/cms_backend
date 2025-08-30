using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.QuickAccess
{
    public class QuickAccessEntityTypeConfig : IEntityTypeConfiguration<Domain.Entities.QuickAccess.QuickAccess>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.QuickAccess.QuickAccess> builder)
        {
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Link).IsRequired();
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}