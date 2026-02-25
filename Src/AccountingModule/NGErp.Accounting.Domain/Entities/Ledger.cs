using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class Ledger :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Ledger>
{
    public int Code { get; private set; }
    public string Name { get; private set; } = default!;
    public bool IsLeading { get; private set; } = false;

    public void Map(EntityTypeBuilder<Ledger> builder)
    {
        builder
            .ToTable(nameof(Ledger), "Accounting");

        builder
            .Property(e => e.Code);

        builder
            .Property(e => e.Name)
            .HasMaxLength(100);

        builder
            .Property(e => e.IsLeading)
            .HasDefaultValue(false);
    }
}
