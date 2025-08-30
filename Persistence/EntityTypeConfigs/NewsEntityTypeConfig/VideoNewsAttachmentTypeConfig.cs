using Domain.Entities.News;
using Domain.Entities.News.PhotoNews;
using Domain.Entities.News.VideoNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NewsEntityTypeConfig
{
    public class VideoNewsAttachmentTypeConfig : IEntityTypeConfiguration<VideoNewsAttachment>
    {
        public void Configure(EntityTypeBuilder<VideoNewsAttachment> builder)
        {
            builder.Property(t => t.VideoPath).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}