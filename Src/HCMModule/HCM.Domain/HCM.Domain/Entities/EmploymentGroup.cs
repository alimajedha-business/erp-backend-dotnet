using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class EmploymentGroup : BaseEntityWithCompany, IBaseEntityTypeConfiguration<EmploymentGroup>
{
    public void Map(EntityTypeBuilder<EmploymentGroup> builder)
    {
        builder.ToTable("EmploymentGroup", "HCM");
    }
}