using AutoMapper.Configuration;
using Domain.Entities.Gallery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.GalleryEntityTypeConfig
{
    public class GalleryEntityTypeConfig : IEntityTypeConfiguration<Gallery>
    {
        public void Configure(EntityTypeBuilder<Gallery> builder)
        {
            builder.Property(p => p.Title)
                .IsRequired();
            builder.Property(p => p.ImagePath)
                .IsRequired();
            builder.HasQueryFilter(f => EF.Property<bool>(f, "IsRemoved") != true);
        }
    }
}