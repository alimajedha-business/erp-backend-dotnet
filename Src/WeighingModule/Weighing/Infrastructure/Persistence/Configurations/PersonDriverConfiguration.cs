using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weighing.Domain.Entities;
using Weighing.Infrastructure.Persistence.Common;

namespace Weighing.Infrastructure.Persistence.Configurations
{
    public class PersonDriverConfiguration : BaseConfiguration<PersonDriver>
    {

        public override void Configure(EntityTypeBuilder<PersonDriver> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.VehicleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.PlateNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.InitialWeight)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DriverType)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(x => x.Person)
                .WithMany()
                .HasForeignKey(x => x.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.VehicleType)
            //    .WithMany()
            //    .HasForeignKey(x => x.VehicleTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
