using Accounting.Domain.Entities;
using Accounting.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NGErp.Accounting.Infrastructure.DataAccess.Repositories.Configuration
{
    public class LedgerConfiguration : IEntityTypeConfiguration<AccountingDbContext>
    {
        public void Configure(EntityTypeBuilder<AccountingDbContext> builder)
        {
            builder.HasData
            (
            new Ledger
            {
                Id = 1,
                Code = 1,
                Name = "دفتر کل اصلی",
                Name2 = "Main Ledger",
                Descripion = "",
                IsLeading = true,
                CreatedAt= DateTime.UtcNow,

            }
            );
        }
    }
}
