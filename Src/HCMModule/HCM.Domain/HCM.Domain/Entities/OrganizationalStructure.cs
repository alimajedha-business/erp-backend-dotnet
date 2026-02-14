using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.HCM.Domain.Entities;

public class OrganizationalStructure : BaseEntityWithCompany, IBaseEntityTypeConfiguration<OrganizationalStructure>
{
    public DateOnly EffectiveFrom { get; set; }
    public string? Description { get; set; }

    public ICollection<OrganizationalStructureItem>? Items { get; set; }

    public void Map(EntityTypeBuilder<OrganizationalStructure> builder)
    {
        builder
            .ToTable(nameof(OrganizationalStructure), "HCM");
    }
}