using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Models.DataAccess;

namespace TaskManager.DataAccess.TypeConfigurations
{
    public class TaskDbModelTypeConfiguration : DbModelTypeConfiguration<TaskDbModel>
    {
        public override void Configure(EntityTypeBuilder<TaskDbModel> builder)
        {
            builder.HasOne(model => model.User).WithMany(u => u.Tasks);
            base.Configure(builder);
            builder.ToTable("Tasks");
        }
    }
}
