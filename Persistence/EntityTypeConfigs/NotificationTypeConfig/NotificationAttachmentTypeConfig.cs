using Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.NotificationTypeConfig
{
    public class NotificationAttachmentTypeConfig : IEntityTypeConfiguration<NotificationAttachment>
    {
        public void Configure(EntityTypeBuilder<NotificationAttachment> builder)
        {
            builder.Property(x => x.AttachmentFile).IsRequired();
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}