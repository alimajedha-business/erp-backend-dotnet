// Ignore Spelling: HCM

using HCM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Infrastructure.DataAccess.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(be => be.Id);

            builder.HasOne(be => be.Company)
               .WithMany()
               .HasForeignKey(be => be.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(be => be.Creator)
                .WithMany()
                .HasForeignKey(be => be.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(be => be.Modifier)
                .WithMany()
                .HasForeignKey(be => be.ModifierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
