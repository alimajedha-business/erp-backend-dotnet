using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceLineAttributeValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceLineAttributeValue>
{
    public Guid RemittanceLineId { get; set; }
    public Guid ItemAttributeId { get; set; }

    public string? StringValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }

    public RemittanceLine RemittanceLine { get; set; } = null!;
    public ItemAttribute ItemAttribute { get; set; } = null!;

    public void Map(EntityTypeBuilder<RemittanceLineAttributeValue> builder)
    {
        builder.ToTable(nameof(RemittanceLineAttributeValue), "Warehouse");

        builder.HasIndex(i => new { i.RemittanceLineId, i.ItemAttributeId }).IsUnique();
        builder.Property(e => e.DecimalValue).HasPrecision(22, 4);

        builder.HasOne(e => e.RemittanceLine)
            .WithMany(e => e.RemittanceLineAttributeValues)
            .HasForeignKey(e => e.RemittanceLineId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(e => e.ItemAttribute)
            .WithMany()
            .HasForeignKey(e => e.ItemAttributeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
