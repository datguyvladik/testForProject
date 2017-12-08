using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models.DataAccess;
using TaskManager.DataAccess.TypeConfigurations;

namespace TaskManager.DataAccess
{
    public class ApplicationContext : IdentityDbContext<UserDbModel>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<TaskDbModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TaskDbModelTypeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
