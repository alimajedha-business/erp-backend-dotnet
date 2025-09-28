using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Infrastructure.DataAccess.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.HasKey(e => e.Id);

            builder.Property(e => e.CompanyId)
               .IsRequired();

            builder.HasOne(v => v.Company)
               .WithMany()
               .HasForeignKey(v => v.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
        }
             
    }
}
