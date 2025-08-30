using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.RelatedLink
{
    public class RelatedLinkEntityTypeConfig: IEntityTypeConfiguration<Domain.Entities.RelatedLink.RelatedLink>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.RelatedLink.RelatedLink> builder)
        {
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Link).IsRequired();
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}