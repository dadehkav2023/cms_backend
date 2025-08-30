using Domain.Entities.News;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NewsEntityTypeConfig
{
    public class NewsTypeConfig : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.Property(t => t.Title).IsRequired();
            
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}