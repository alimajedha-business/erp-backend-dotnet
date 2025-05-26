using Accounting.Domain.Entities;
using Accounting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Accounting.Infrastructure.DataAccess.Repositories.Configuration
{
    public class LedgerConfiguration : IEntityTypeConfiguration<Ledger>
    {
        public void Configure(EntityTypeBuilder<Ledger> builder)
        {
            builder.HasData
            (
            new Ledger
            {
                Id = 1,
                Code = 1,
                Name = "دفتر کل اصلی",
                Name2 = "Main Ledger",
                Description = "",
                IsLeading = true,
                CreatedAt= DateTime.UtcNow,
            }
            );           
        }
    }
}
