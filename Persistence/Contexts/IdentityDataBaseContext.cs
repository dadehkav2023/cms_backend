using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity.Role;
using Domain.Entities.Identity.User;
using Persistence.ContextConfig.OnModelCreatingConfigs;

namespace Persistence.Contexts
{
    public class IdentityDataBaseContext : IdentityDbContext<User,Role,int>
    {
        public IdentityDataBaseContext(DbContextOptions<IdentityDataBaseContext> options) : base(options)
        {
        }


        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new OnRoleModelCreatingConfig());
            base.OnModelCreating(builder);
        }
    }
}
