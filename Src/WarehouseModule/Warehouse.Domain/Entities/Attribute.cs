using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class Attribute :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Attribute>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public AttributeDataType DataType { get; private set; }
    public bool IsItemAttribute { get; private set; } = false;
    public bool IsRequired { get; private set; } = false;
    public bool IsStockDimension {  get; private set; } = false;

    public void Map(EntityTypeBuilder<Attribute> builder)
    {
        builder
            .ToTable(nameof(Attribute), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId, i.Code })
            .IsUnique()
            .HasDatabaseName("UX_Attribute_Company_Code");

        builder
            .Property(e => e.Code)
            .HasMaxLength(20);

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);

        builder
            .Property(e => e.IsItemAttribute)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsRequired)
            .HasDefaultValue(false);

        builder
            .Property(e => e.IsStockDimension)
            .HasDefaultValue(false);
    }
}

public enum AttributeDataType
{
    Text = 0,
    Int,
    Decimal,
    Date,
    Bool,
    Enum
}