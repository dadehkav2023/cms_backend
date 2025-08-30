using Domain.Entities.Rules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.Rules
{
    public class RulesAttachmentTypeConfig : IEntityTypeConfiguration<RulesAttachment>
    {
        public void Configure(EntityTypeBuilder<RulesAttachment> builder)
        {
            builder.Property(x => x.FilePath).IsRequired();
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}