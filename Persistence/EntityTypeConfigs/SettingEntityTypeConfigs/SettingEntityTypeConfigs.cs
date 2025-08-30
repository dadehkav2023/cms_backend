using Domain.Entities.CMS.Setting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityTypeConfigs.SettingEntityTypeConfigs
{
    public class SettingEntityTypeConfigs : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(p => p.Name)
                 .IsRequired()
                 .HasMaxLength(500);

            builder.Property(p => p.LogoImageAddress)
                 .IsRequired();

            builder.Property(p => p.Address)
                 .IsRequired();

            builder.Property(p => p.PostalCode)
                .IsRequired();

            builder.Property(p => p.LogoImageAddress)
                .IsRequired();


            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") != true);
        }
    }
}
