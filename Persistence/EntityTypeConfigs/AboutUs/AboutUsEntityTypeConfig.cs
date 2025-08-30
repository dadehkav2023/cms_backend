using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.AboutUs
{
    public class AboutUsEntityTypeConfig: IEntityTypeConfiguration<Domain.Entities.AboutUs.AboutUs>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.AboutUs.AboutUs> builder)
        {
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);

        }
    }
}