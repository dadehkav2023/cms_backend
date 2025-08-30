using Domain.Entities.Article;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.ArticleTypeConfig
{
    public class ArticleAttachmentTypeConfig : IEntityTypeConfiguration<ArticleAttachment>
    {
        public void Configure(EntityTypeBuilder<ArticleAttachment> builder)
        {
            builder.Property(x => x.AttachmentFile).IsRequired();
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}