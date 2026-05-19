using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record FeatureSettingsDto(
    Guid Id,
    int MaxCategoryLevel,
    WarehouseSerialRule WarehouseSerialRule,
    string WarehouseSerialRuleDescription,
    int PriceRoundingPoint,
    Guid AccountingFiscalYear,
    Guid PropertyFiscalYear,
    Guid WarehousePreviousFiscalYear
)
{
    public static string GetWarehouseSerialRuleDescription(
        WarehouseSerialRule value
    )
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public class CreateFeatureSettingsDto
{
    public int MaxCategoryLevel { get; set; }
    public WarehouseSerialRule WarehouseSerialRule { get; set; }
    public int PriceRoundingPoint { get; set; }
    public Guid AccountingFiscalYear { get; set; }
    public Guid PropertyFiscalYear { get; set; }
    public Guid WarehousePreviousFiscalYear { get; set; }
}

public class PatchFeatureSettingsDto
{
    public int? MaxCategoryLevel { get; set; }
    public WarehouseSerialRule? WarehouseSerialRule { get; set; }
    public int? PriceRoundingPoint { get; set; }
    public Guid? AccountingFiscalYear { get; set; }
    public Guid? PropertyFiscalYear { get; set; }
    public Guid? WarehousePreviousFiscalYear { get; set; }
}
