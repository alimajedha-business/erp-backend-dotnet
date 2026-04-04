using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public enum MonthTypeEnum
{ Normal = 1, ThirtyDay }

public class EmploymentGroupSpecification : BaseEntity, IBaseEntityTypeConfiguration<EmploymentGroupSpecification>
{
    public Guid EmploymentGroupId { get; set; }
    public MonthTypeEnum MonthType { get; set; }
    public int WorkMinutes { get; set; }
    public int ValidFrom { get; set; }
    public int ValidTo { get; set; } = 0;

    public void Map(EntityTypeBuilder<EmploymentGroupSpecification> builder)
    {
        builder.ToTable(nameof(EmploymentGroupSpecification), "HCM");
    }
}