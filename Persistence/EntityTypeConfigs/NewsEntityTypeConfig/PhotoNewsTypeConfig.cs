using Domain.Entities.News;
using Domain.Entities.News.PhotoNews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NewsEntityTypeConfig
{
    public class PhotoNewsTypeConfig : IEntityTypeConfiguration<PhotoNews>
    {
        public void Configure(EntityTypeBuilder<PhotoNews> builder)
        {
            builder.Property(t => t.ImagePath).IsRequired();
            builder.Property(t => t.Title).IsRequired();
            builder.Property(t => t.Summary).IsRequired();
            builder.Property(t => t.Summary).HasMaxLength(2000);
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}