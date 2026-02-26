using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.General.Domain.Entities;

public class CompanyUnit :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<CompanyUnit>
{
    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    public void Map(EntityTypeBuilder<CompanyUnit> builder)
    {
        builder
            .ToTable(nameof(CompanyUnit), "General");

        builder
            .Property(e => e.Code)
            .HasMaxLength(50);

        builder
            .Property(e => e.Name)
            .HasMaxLength(250);

        builder
            .Property(e => e.IsActive)
            .HasDefaultValue(true);
    }
}
