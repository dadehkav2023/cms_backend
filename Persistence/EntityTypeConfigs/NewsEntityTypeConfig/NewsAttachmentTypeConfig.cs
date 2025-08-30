using Domain.Entities.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NewsEntityTypeConfig
{
    public class NewsAttachmentTypeConfig : IEntityTypeConfiguration<NewsAttachment>
    {
        public void Configure(EntityTypeBuilder<NewsAttachment> builder)
        {
            builder.Property(x => x.AttachmentFile).IsRequired();
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}