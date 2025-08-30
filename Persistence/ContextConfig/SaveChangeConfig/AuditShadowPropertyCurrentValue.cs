using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.ContextConfig.SaveChangeConfig
{
    public class AuditShadowPropertyCurrentValue : DbContext
    {
        public void Save()
        {
            var modifiedEntitis = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added ||
                            p.State == EntityState.Modified ||
                            p.State == EntityState.Deleted);

            foreach (var item in modifiedEntitis)
            {
                var entity = item.Context.Model.FindEntityType(item.Entity.GetType());
                if (entity == null) continue;

                var insert = entity.FindProperty("InsertTime");
                var update = entity.FindProperty("UpdateTime");
                var remove = entity.FindProperty("RemoveTime");
                var isremoved = entity.FindProperty("IsRemoved");

                if (item.State == EntityState.Added && insert != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Modified && update != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && remove != null && isremoved != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                }
            }

        }
    }
}
