using Domain.Entities.Gallery;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.Province
{
    public class ProvinceEntityTypeConfig : IEntityTypeConfiguration<Domain.Entities.Map.Map>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Map.Map> builder)
        {
            builder.Property(p => p.Province)
                .IsRequired();
            builder.Property(p => p.WebsiteAddress)
                .IsRequired();


            builder.HasQueryFilter(f => EF.Property<bool>(f, "IsRemoved") != true);
        }
    }
}