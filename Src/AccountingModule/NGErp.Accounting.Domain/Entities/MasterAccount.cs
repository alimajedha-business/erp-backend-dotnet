using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class MasterAccount :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<MasterAccount>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public string AuthorizedUsers { get; private set; } = default!;
    public Guid? LedgerId { get; private set; }

    public Ledger? Ledger { get; private set; }

    public void Map(EntityTypeBuilder<MasterAccount> builder)
    {
        builder
            .ToTable(nameof(MasterAccount), "Accounting");

        builder
            .Property(e => e.Code)
            .HasMaxLength(20);

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(e => e.AuthorizedUsers)
            .HasMaxLength(1000);

        builder
            .HasOne(e => e.Ledger)
            .WithMany()
            .HasForeignKey(e => e.LedgerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
