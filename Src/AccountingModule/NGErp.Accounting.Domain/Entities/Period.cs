using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class Period :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Period>
{
    public string Name { get; private set; } = default!;
    public virtual DateTime StartDate { get; private set; }
    public virtual DateTime EndDate { get; private set; }
    public Guid? PreviousPeriodId { get; private set; }

    public Period? PreviousPeriod { get; set; }

    public void Map(EntityTypeBuilder<Period> builder)
    {
        builder
            .ToTable(nameof(Period), "Accounting");

        builder
            .Property(e => e.Name)
            .HasMaxLength(100);

        builder
            .Property<DateTime>(nameof(StartDate))
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .Property<DateTime>(nameof(EndDate))
            .HasColumnType("datetime2")
            .HasDefaultValueSql("GETUTCDATE()");

        builder
            .HasOne(e => e.PreviousPeriod)
            .WithMany()
            .HasForeignKey(e => e.PreviousPeriodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
