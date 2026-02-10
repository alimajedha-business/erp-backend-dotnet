using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class OrganizationalStructureItem : BaseEntityWithCompany, IBaseEntityTypeConfiguration<OrganizationalStructureItem>
{
    public Guid OrganizationalStructureId { get; set; }
    public Guid NodeId { get; set; }
    public Guid? ParentItemId { get; set; }

    public required OrganizationalStructure OrganizationalStructure { get; set; }
    public required OrganizationNode Node { get; set; }
    public OrganizationalStructureItem? ParentItem { get; set; }

    public required ICollection<OrganizationalStructureItem> Children { get; set; }

    public void Map(EntityTypeBuilder<OrganizationalStructureItem> builder)
    {
        builder
             .ToTable(nameof(OrganizationalStructureItem), "HCM");

        builder.HasOne(e => e.OrganizationalStructure)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.OrganizationalStructureId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Node)
            .WithMany()
            .HasForeignKey(e => e.NodeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.ParentItem)
            .WithMany()
            .HasForeignKey(e => e.ParentItemId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}