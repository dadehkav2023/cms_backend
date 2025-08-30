using Domain.Entities.Slider;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.EntityTypeConfigs.SliderEntityTypeConfigs
{
    public class SliderEntityTypeConfigs : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(p => p.Title)
                .IsRequired();
            builder.Property(p => p.ImagePath)
                .IsRequired();
            builder.Property(p => p.SortOrder)
                .IsRequired();
            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") != true);
        }
    }
}
