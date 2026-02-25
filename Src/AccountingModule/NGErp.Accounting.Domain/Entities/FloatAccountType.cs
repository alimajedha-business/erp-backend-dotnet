using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class FloatAccountType :
    BaseEntity,
    IBaseEntityTypeConfiguration<FloatAccountType>
{
    public string NameFa { get; private set; } = default!;
    public string NameEn { get; private set; } = default!;
    public bool IsStatic { get; private set; }
    public int VoucherItemFloatLevel { get; private set; }
    public Guid? ParentId { get; private set; }

    public FloatAccountType? Parent { get; set; }

    public void Map(EntityTypeBuilder<FloatAccountType> builder)
    {
        builder
            .ToTable(nameof(FloatAccountType), "Accounting");

        builder
            .Property(e => e.NameFa)
            .HasMaxLength(100);

        builder
            .Property(e => e.NameEn)
            .HasMaxLength(100);

        builder
            .Property(e => e.IsStatic)
            .HasDefaultValue(false);

        builder
            .Property(e => e.VoucherItemFloatLevel)
            .HasDefaultValue(1);

        builder
            .HasOne(e => e.Parent)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
