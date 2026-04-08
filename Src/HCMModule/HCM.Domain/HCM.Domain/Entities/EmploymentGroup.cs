using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmploymentGroup : BaseEntityWithCompany, IBaseEntityTypeConfiguration<EmploymentGroup>
{
    public required string Name { get; set; }

    public ICollection<EmploymentGroupSpecification> Specifications { get; set; } = new List<EmploymentGroupSpecification>();

    public void Map(EntityTypeBuilder<EmploymentGroup> builder)
    {
        builder.ToTable(nameof(EmploymentGroup), "HCM")
            .ToTable(t => t.HasCheckConstraint(
                "CK_Name_Min_Length",
                "LEN(Name) >= 2"
                ));
        builder.Property<string>(e => e.Name).HasMaxLength(500);
    }
}