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
    public class WarehouseStockConfiguration : BaseConfiguration<WarehouseStock>
    {
        public override void Configure(EntityTypeBuilder<WarehouseStock> builder)
        {
            base.Configure(builder);

            builder.Property(w => w.MaxAssetValue)
                .HasPrecision(18, 2);
        }
    }
}
