using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.ContactUs
{
    public class ContactUsEntityTypeConfig: IEntityTypeConfiguration<Domain.Entities.ContactUs.ContactUs>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ContactUs.ContactUs> builder)
        {
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);

        }
    }
}