using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weighing.Domain.Entities;
using Weighing.Infrastructure.Persistence.Common;

namespace Weighing.Infrastructure.Persistence.Configurations
{
    public class DischargeStationConfiguration : BaseConfiguration<DischargeStation>
    {
        public override void Configure(EntityTypeBuilder<DischargeStation> builder)
        {
            base.Configure(builder);

            
            builder.Property(e => e.Code)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
       
    }
}
