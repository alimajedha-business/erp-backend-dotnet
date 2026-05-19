using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class RemittanceFieldValue :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<RemittanceFieldValue>
{
    public Guid RemittanceId { get; set; }
    public Guid? RemittanceLineId { get; set; }
    public Guid FieldDefinitionId { get; set; }
    public string? StringValue { get; set; }
    public int? IntValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public DateOnly? DateValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public Guid? ReferenceId { get; set; }
    public bool? BooleanValue { get; set; }

    public Remittance Remittance { get; set; } = default!;
    public RemittanceLine? RemittanceLine { get; set; }
    public ReceiptFieldDefinition FieldDefinition { get; set; } = default!;

    public void Map(EntityTypeBuilder<RemittanceFieldValue> builder)
    {
        builder.ToTable(nameof(RemittanceFieldValue), "Warehouse");

        builder.HasIndex(i => new { i.RemittanceId, i.FieldDefinitionId })
            .IsUnique()
            .HasFilter("[RemittanceLineId] IS NULL");

        builder.HasIndex(i => new { i.RemittanceLineId, i.FieldDefinitionId })
            .IsUnique()
            .HasFilter("[RemittanceLineId] IS NOT NULL");

        builder.Property(e => e.DecimalValue).HasPrecision(22, 4);

        builder.HasOne(e => e.Remittance)
            .WithMany(e => e.RemittanceFieldValues)
            .HasForeignKey(e => e.RemittanceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.RemittanceLine)
            .WithMany(e => e.RemittanceFieldValues)
            .HasForeignKey(e => e.RemittanceLineId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.FieldDefinition)
            .WithMany()
            .HasForeignKey(e => e.FieldDefinitionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
