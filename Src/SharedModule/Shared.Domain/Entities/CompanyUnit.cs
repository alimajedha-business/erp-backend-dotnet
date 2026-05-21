using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Shared.Domain.Entities;

[Table("company_units", Schema = "Shared")]
public class CompanyUnit : BaseEntity, IBaseEntityTypeConfiguration<CompanyUnit>
{
    [Column("company_id")]
    public Guid CompanyId { get; set; }

    [ForeignKey(nameof(CompanyId))]
    public virtual Company Company { get; set; } = default!;

    [Column("parent_id")]
    public Guid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public virtual CompanyUnit? Parent { get; set; }

    [Column("level")]
    public int Level { get; set; }

    [Column("last_level")]
    public bool LastLevel { get; set; }

    [Column("name")]
    public string Name { get; set; } = default!;

    [Column("code")]
    public string Code { get; set; } = default!;

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("authorized_users")]
    public string? AuthorizedUsers { get; set; }

    public virtual ICollection<CompanyUnit> Children { get; set; } = new List<CompanyUnit>();

    public void Map(EntityTypeBuilder<CompanyUnit> builder)
    {
    }
}
