using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

internal class Attribute :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<Attribute>
{
    public string Code { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public AttributeDataType DataType { get; private set; }

    public void Map(EntityTypeBuilder<Attribute> builder)
    {
        builder
            .ToTable(nameof(Attribute), "Warehouse")
            .ToTable(t => t.HasCheckConstraint(
                "CK_ATTRIBUTE_DATATYPE",
                "DataType IN N'Text', N'Int', N'Decimal', N'Date', N'Bool', N'Enum'"
            ));

        builder
            .Property(e => e.Code)
            .HasMaxLength(20);

        builder
            .Property(e => e.Title)
            .HasMaxLength(50);
    }
}

public enum AttributeDataType
{
    Text = 0,
    Int,
    Decimal,
    DAte,
    Bool,
    Enum
}