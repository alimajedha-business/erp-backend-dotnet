using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class SlaveAccount :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<SlaveAccount>
{
    public string Title { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public Guid MasterId { get; private set; }
    public Guid LedgerId { get; private set; }
    public Guid? ParentId { get; private set; }

    public required MasterAccount Master { get; set; }
    public required Ledger Ledger { get; set; }
    public SlaveAccount? Parent { get; set; }

    public void Map(EntityTypeBuilder<SlaveAccount> builder)
    {
        builder
            .ToTable(nameof(SlaveAccount), "Accounting");

        builder
            .Property(e => e.Title)
            .HasMaxLength(100);

        builder
            .Property(e => e.Type)
            .HasDefaultValue(true);

        builder
            .HasOne(e => e.Master)
            .WithMany()
            .HasForeignKey(e => e.MasterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Ledger)
            .WithMany()
            .HasForeignKey(e => e.LedgerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Parent)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
