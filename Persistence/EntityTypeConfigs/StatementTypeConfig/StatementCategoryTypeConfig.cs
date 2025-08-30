using Domain.Entities.Statement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityTypeConfigs.StatementTypeConfig
{
    public class StatementCategoryTypeConfig : IEntityTypeConfiguration<StatementCategory>
    {
        public void Configure(EntityTypeBuilder<StatementCategory> builder)
        {
            builder.HasQueryFilter(n => EF.Property<bool>(n, "IsRemoved") != true);
        }
    }
}
