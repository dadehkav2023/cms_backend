using System;
using System.Linq;
using Domain.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Persistence.ContextConfig.OnModelCreatingConfigs
{
    public class ReflectionEntitis
    {
        public static void Execute(ModelBuilder builder)
        {

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime?>("InsertTime");
                    builder.Entity(entityType.Name).Property<int>("InsertBy");
                    builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    builder.Entity(entityType.Name).Property<int>("UpdateBy");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    builder.Entity(entityType.Name).Property<bool?>("IsRemoved");
                }
            }
        }
        
        public static void FillShadowProperties(ChangeTracker changeTracker)
        {
            var entries = changeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added &&
                    entry.Metadata.FindProperty("InsertTime") != null)
                {
                    entry.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                else if(entry.State == EntityState.Modified &&
                        entry.Metadata.FindProperty("UpdateTime") != null)
                {
                    entry.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
            }
        }
        
    }
}
