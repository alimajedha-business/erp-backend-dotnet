using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class WorkLocation :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<WorkLocation>
{
    public Guid? ParentId { get; set; }
    public string Title { get; set; } = default!;
    public WorkLocation? Parent { get; set; }
    public void Map(EntityTypeBuilder<WorkLocation> builder)
    {
        builder
            .ToTable(nameof(WorkLocation), "HCM");

        builder
            .HasIndex(e => e.ParentId)
            .HasDatabaseName("IX_WorkLocation_Parent");
        builder
            .HasIndex(e => new { e.CompanyId, e.Title })
            .IsUnique()
            .HasDatabaseName("UX_WorkLocation_CompanyId_Title");

        builder
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder
            .HasOne(e => e.Parent)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
