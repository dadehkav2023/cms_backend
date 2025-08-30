using Application.Interfaces.IRepositories;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Domain.Attributes.Rules;
using Persistence.ContextConfig.OnModelCreatingConfigs;
using Persistence.EntityTypeConfigs.Rules;

namespace Persistence.Contexts
{
    public class RulesDataBaseContext : DbContext , IUnitOfWorkRules, IMyContext
    {

        private IDictionary<Type, object> _repositories;

        public RulesDataBaseContext(DbContextOptions<RulesDataBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Config(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Config(ModelBuilder modelBuilder)
        {
            LoadEntities(modelBuilder);
            //Reflection Shadow Property
            ReflectionEntitis.Execute(modelBuilder);

            modelBuilder.ApplyConfiguration(new RulesTypeConfig());
            modelBuilder.ApplyConfiguration(new RulesAttachmentTypeConfig());
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            Type entityType = typeof(TEntity);
            if (_repositories.Keys.Contains(entityType) == true)
            {
                return _repositories[entityType] as IRepository<TEntity>;
            }
            IRepository<TEntity> repository = new Repository<TEntity>(this);
            _repositories.Add(entityType, repository);
            return repository;
        }


        private static void LoadEntities(ModelBuilder modelBuilder)
        {

            var asmPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameof(Domain) + ".dll";
            var modelInAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(asmPath);
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });
            foreach (var type in modelInAssembly.ExportedTypes)
            {
                var typeFind = type.CustomAttributes.FirstOrDefault();
                if (typeFind != null && typeFind.AttributeType.Name == nameof(RulesEntityAttribute))
                    entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }

        public override int SaveChanges()
        {
            #region SaveAuditAble
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
                    item.Property("IsRemoved").CurrentValue = false;
                }
                if (item.State == EntityState.Modified && update != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && remove != null && isremoved != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                    item.State = EntityState.Modified;
                }
            }
            #endregion
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            #region SaveAuditAble
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
                    item.Property("IsRemoved").CurrentValue = false;
                }
                if (item.State == EntityState.Modified && update != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && remove != null && isremoved != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                    item.State = EntityState.Modified;
                }
            }
            #endregion
            return base.SaveChangesAsync().Result;
        }
    }
}
