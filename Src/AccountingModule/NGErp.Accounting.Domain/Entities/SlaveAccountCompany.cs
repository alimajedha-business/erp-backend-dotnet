using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class SlaveAccountCompany :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<SlaveAccountCompany>
{
    public bool NeedTtms { get; private set; }
    public bool IsActive { get; private set; } = true;
    public virtual DateTime DueDate { get; private set; }
    public Guid MasterId { get; private set; }
    public Guid SlaveId { get; private set; }
    public Guid LedgerId { get; private set; }

    public required MasterAccount Master { get; set; }
    public required SlaveAccount Slave { get; set; }
    public required Ledger Ledger { get; set; }

    public void Map(EntityTypeBuilder<SlaveAccountCompany> builder)
    {
        builder
            .ToTable(nameof(SlaveAccountCompany), "Accounting");

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .HasOne(e => e.Master)
            .WithMany()
            .HasForeignKey(e => e.MasterId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Slave)
            .WithMany()
            .HasForeignKey(e => e.SlaveId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Ledger)
            .WithMany()
            .HasForeignKey(e => e.LedgerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
