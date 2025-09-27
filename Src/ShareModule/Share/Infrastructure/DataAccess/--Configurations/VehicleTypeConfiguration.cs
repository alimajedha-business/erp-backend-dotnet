using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.DataAccess.Configurations
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.CompanyId)
                .IsRequired();

            builder.HasIndex(e => new { e.CompanyId, e.Name }, "vehicle_types_company_id_name_uniq")
                .IsUnique()
                .HasFilter("([company_id] IS NOT NULL AND [name] IS NOT NULL)");
        }
    }
}
