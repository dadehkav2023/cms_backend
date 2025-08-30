using Domain.Entities.ContactUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.ContactUs
{
    public class ContactUsMessageEntityTypeConfig : IEntityTypeConfiguration<ContactUsMessage>
    {
        public void Configure(EntityTypeBuilder<ContactUsMessage> builder)
        {
            builder.Property(p => p.TextMessage).IsRequired();

            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}