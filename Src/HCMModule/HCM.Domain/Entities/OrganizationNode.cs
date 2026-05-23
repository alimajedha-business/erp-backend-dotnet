using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum NodeType
{
    Department = 1,
    Position = 2
}

public class OrganizationNode : BaseEntityWithCompany, IBaseEntityTypeConfiguration<OrganizationNode>
{
    public Guid? DepartmentId { get; set; }
    public Guid? PositionId { get; set; }
    public NodeType NodeType { get; set; } // Department or Position

    public Department? Department { get; set; }
    public Position? Position { get; set; }

    // public ICollection<OrganizationalStructureItem>? StructureItems { get; set; }
    
    public void Map(EntityTypeBuilder<OrganizationNode> builder)
    {
        builder.ToTable(nameof(OrganizationNode), "HCM", t =>
        {
            t.HasCheckConstraint(
                "CK_OrganizationNode_OnlyOneReference",
                @"(
                ([DepartmentId] IS NOT NULL AND [PositionId] IS NULL AND [NodeType] = 1)
                OR
                ([DepartmentId] IS NULL AND [PositionId] IS NOT NULL AND [NodeType] = 2)
            )");
        });

        builder.HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.Position)
            .WithMany()
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(e => new { e.CompanyId, e.DepartmentId })
            .IsUnique()
            .HasFilter("[DepartmentId] IS NOT NULL");

        builder.HasIndex(e => new { e.CompanyId, e.PositionId })
            .IsUnique()
            .HasFilter("[PositionId] IS NOT NULL");
    }
}