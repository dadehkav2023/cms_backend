using Domain.Entities.Notification;
using Domain.Entities.Statement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigs.StatementTypeConfig
{
    public class StatementAttachmentTypeConfig : IEntityTypeConfiguration<StatementAttachment>
    {
        public void Configure(EntityTypeBuilder<StatementAttachment> builder)
        {
            builder.Property(x => x.AttachmentFile).IsRequired();
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}