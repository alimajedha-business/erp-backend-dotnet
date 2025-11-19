using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weighing.Domain.Entities;

namespace Weighing.Infrastructure.DataAccess.Configurations
{
    public class PackageTypeConfiguration : BaseConfiguration<PackageType>
    {

        public override void Configure(EntityTypeBuilder<PackageType> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Code)
               .HasMaxLength(10)
               .IsRequired();

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(250);
        }
    }
}
