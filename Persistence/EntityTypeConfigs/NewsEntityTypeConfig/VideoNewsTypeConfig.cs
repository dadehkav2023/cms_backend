using Domain.Entities.News;
using Domain.Entities.News.PhotoNews;
using Domain.Entities.News.VideoNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NewsEntityTypeConfig
{
    public class VideoNewsTypeConfig : IEntityTypeConfiguration<VideoNews>
    {
        public void Configure(EntityTypeBuilder<VideoNews> builder)
        {
            builder.Property(t => t.ImagePath).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Summary).IsRequired();
            builder.Property(t => t.Summary).HasMaxLength(2000);
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}