using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Attributes.Store;
using Domain.Entities.Identity.Role;
using Domain.Entities.Identity.User;
using Infrastructure.DataAccess.Repositories;
using Persistence.ContextConfig.OnModelCreatingConfigs;
using Persistence.EntityTypeConfigs.StoreConfig;

namespace Persistence.Contexts
{
    public class IdentityDataBaseContext : IdentityDbContext<User,Role,int>, IUnitOfWork, IMyContext
    {
        private IDictionary<Type, object> _repositories;

        public IdentityDataBaseContext(DbContextOptions<IdentityDataBaseContext> options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     builder.ApplyConfiguration(new OnRoleModelCreatingConfig());
        //     base.OnModelCreating(builder);
        // }
        
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

            modelBuilder.ApplyConfiguration(new OrderConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }


        private static void LoadEntities(ModelBuilder modelBuilder)
        {

            var asmPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + nameof(Domain) + ".dll";
            var modelInAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(asmPath);
            var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });
            foreach (var type in modelInAssembly.ExportedTypes)
            {
                var typeFind = type.CustomAttributes.FirstOrDefault();
                if (typeFind != null && typeFind.AttributeType.Name == nameof(MainEntityAttribute))
                    entityMethod.MakeGenericMethod(type).Invoke(modelBuilder, new object[] { });
            }
        }


        public Task<int> SaveChangesAsync()
        {
            ReflectionEntitis.FillShadowProperties(ChangeTracker);
            return base.SaveChangesAsync();
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
    }
}
