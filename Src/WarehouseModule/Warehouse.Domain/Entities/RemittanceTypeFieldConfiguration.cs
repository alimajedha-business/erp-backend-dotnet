using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceTypeFieldConfiguration :
    BaseEntity,
    IBaseEntityTypeConfiguration<RemittanceTypeFieldConfiguration>
{
    public Guid RemittanceTypeConfigurationId { get; set; }
    public Guid FieldDefinitionId { get; set; }
    public bool Exists { get; set; }
    public bool IsRequired { get; set; }
    public RemittanceConfiguredPlacement Placement { get; set; }

    public RemittanceTypeConfiguration RemittanceTypeConfiguration { get; set; } = null!;
    public RemittanceFieldDefinition FieldDefinition { get; set; } = null!;

    public void Map(EntityTypeBuilder<RemittanceTypeFieldConfiguration> builder)
    {
        builder
            .ToTable(nameof(RemittanceTypeFieldConfiguration), "Warehouse");

        builder.HasIndex(i => new
        {
            i.RemittanceTypeConfigurationId,
            i.FieldDefinitionId
        }).IsUnique();

        builder.HasOne(e => e.RemittanceTypeConfiguration)
            .WithMany(e => e.FieldConfigurations)
            .HasForeignKey(e => e.RemittanceTypeConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.FieldDefinition)
            .WithMany()
            .HasForeignKey(e => e.FieldDefinitionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
