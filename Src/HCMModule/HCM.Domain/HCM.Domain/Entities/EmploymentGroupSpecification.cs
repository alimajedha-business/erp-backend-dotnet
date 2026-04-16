using System.ComponentModel;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum MonthTypeEnum
{
    [Description("Normal")]
    Normal = 1,

    [Description("ThirtyDay")]
    ThirtyDay
}

public class EmploymentGroupSpecification : BaseEntity, IBaseEntityTypeConfiguration<EmploymentGroupSpecification>
{
    public Guid EmploymentGroupId { get; set; }
    public MonthTypeEnum MonthType { get; set; }
    public int WorkMinutes { get; set; } = 220;
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }

    public void Map(EntityTypeBuilder<EmploymentGroupSpecification> builder)
    {
        builder.ToTable(nameof(EmploymentGroupSpecification), "HCM", t =>
            {
                t.HasCheckConstraint(
                    "CK_EmploymentGroupSpecification_MonthType",
                    "[MonthType] Between 1 and 2");

                t.HasCheckConstraint(
                   "CK_EmploymentGroupSpecification_WorkMinutes",
                   "[WorkMinutes] >=0 AND [WorkMinutes] <= 720");

                t.HasCheckConstraint(
                           "CK_EmploymentGroupSpecification_ValidRange",
                           "[ValidTo] IS NULL OR [ValidTo] >= [ValidFrom]");
            });

        builder.HasOne<EmploymentGroup>()     // principal entity
           .WithMany(e => e.Specifications)
           .HasForeignKey(x => x.EmploymentGroupId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => new
        {
            x.EmploymentGroupId,
            x.ValidFrom,
            x.ValidTo
        }
        ).HasDatabaseName("IX_EmpGroupSpec_Validity");
    }
}