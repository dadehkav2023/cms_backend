using Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.Rules
{
    public class RulesTypeConfig : IEntityTypeConfiguration<Domain.Entities.Rules.Rules>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Rules.Rules> builder)
        {
            builder.Property(t => t.Description).IsRequired();
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}