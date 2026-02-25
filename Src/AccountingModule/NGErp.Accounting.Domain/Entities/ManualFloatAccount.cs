using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Accounting.Domain.Entities;

public class ManualFloatAccount :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<ManualFloatAccount>
{
    public int Code { get; private set; }
    public string Title { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public string AuthorizedUsers { get; private set; } = default!;
    public Guid FloatAccountTypeId { get; private set; }
    public Guid? ParentId { get; private set; }

    public required FloatAccountType FloatAccountType { get; set; }
    public ManualFloatAccount? Parent { get; set; }

    public void Map(EntityTypeBuilder<ManualFloatAccount> builder)
    {
        builder
            .ToTable(nameof(ManualFloatAccount), "Accounting");

        builder
            .Property(e => e.Title)
            .HasMaxLength(100);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder
            .Property(e => e.AuthorizedUsers)
            .HasMaxLength(1000);

        builder
            .HasOne(e => e.FloatAccountType)
            .WithMany()
            .HasForeignKey(e => e.FloatAccountTypeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(e => e.Parent)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
