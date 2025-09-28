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
    public class WarehouseStockConfiguration : IEntityTypeConfiguration<WarehouseStock>
    {
        public void Configure(EntityTypeBuilder<WarehouseStock> builder)
        {
            builder.ToTable("WarehouseStock", "Warehouse");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CompanyId)
               .IsRequired();

            builder.HasOne(p => p.Company)
               .WithMany()
               .HasForeignKey(p => p.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(w => w.CompanyUnitId);
            builder.HasOne(w => w.CompanyUnit)
                .WithMany()
                .HasForeignKey(w => w.CompanyUnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
