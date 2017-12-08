using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Models.DataAccess;

namespace TaskManager.DataAccess.TypeConfigurations
{
    public class DbModelTypeConfiguration<TDbModel> : IEntityTypeConfiguration<TDbModel>
        where TDbModel : DbModel
    {
        public virtual void Configure(EntityTypeBuilder<TDbModel> builder)
        {
            builder.HasKey(model => model.Id);
        }
    }
}
