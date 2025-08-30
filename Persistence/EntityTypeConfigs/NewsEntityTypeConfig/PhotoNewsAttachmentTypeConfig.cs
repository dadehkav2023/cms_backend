using Domain.Entities.News;
using Domain.Entities.News.PhotoNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NewsEntityTypeConfig
{
    public class PhotoNewsAttachmentTypeConfig : IEntityTypeConfiguration<PhotoNewsAttachment>
    {
        public void Configure(EntityTypeBuilder<PhotoNewsAttachment> builder)
        {
            builder.Property(t => t.ImagePath).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}