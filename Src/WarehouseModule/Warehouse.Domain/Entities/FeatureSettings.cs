using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;
using NGErp.General.Domain.Entities;

namespace NGErp.Warehouse.Domain.Entities;

public class FeatureSettings :
    BaseEntityWithCompany,
    IBaseEntityTypeConfiguration<FeatureSettings>
{
    public required int MaxCategoryLevel { get; set; }
    public required WarehouseSerialRule WarehouseSerialRule { get; set; }
    public required int PriceRoundingPoint { get; set; }
    public required Guid AccountingFiscalYear { get; set; }
    public required Guid PropertyFiscalYear { get; set; }
    public required Guid WarehousePreviousFiscalYear { get; set; }

    public void Map(EntityTypeBuilder<FeatureSettings> builder)
    {
        builder
            .ToTable(nameof(FeatureSettings), "Warehouse");

        builder
            .HasIndex(i => new { i.CompanyId })
            .IsUnique()
            .HasDatabaseName("UX_FeatureSettings_Company");
    }
}

[JsonConverter(typeof(WarehouseSerialRuleConverter))]
public enum WarehouseSerialRule
{
    [Description("UniqueInWarehouseSystem")]
    UniqueInWarehouseSystem = 1,

    [Description("UniqueInWarehouseUser")]
    UniqueInWarehouseUser = 2,

    [Description("UniqueInCompanySystem")]
    UniqueInCompanySystem = 3,

    [Description("UniqueInCompanyUser")]
    UniqueInCompanyUser = 4
}

public class WarehouseSerialRuleConverter :
    JsonConverter<WarehouseSerialRule>
{
    public override WarehouseSerialRule Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = reader.GetString();
        return value switch
        {
            "UniqueInWarehouseSystem" => WarehouseSerialRule.UniqueInWarehouseSystem,
            "UniqueInWarehouseUser" => WarehouseSerialRule.UniqueInWarehouseUser,
            "UniqueInCompanySystem" => WarehouseSerialRule.UniqueInCompanySystem,
            "UniqueInCompanyUser" => WarehouseSerialRule.UniqueInCompanyUser,
            _ => throw new JsonException($"Unknown WarehouseSerialRule: {value}")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        WarehouseSerialRule value,
        JsonSerializerOptions options
    )
    {
        writer.WriteStringValue(GetDataTypeDescription(value));
    }

    private static string GetDataTypeDescription(WarehouseSerialRule value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
