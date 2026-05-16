using System.ComponentModel;
using System.Reflection;

using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record SiUnitDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol,
    decimal FactorToBase,
    bool IsBaseUnit,
    UnitDimension UnitDimension,
    string UnitDimensionTitle
)
{
    public static string GetUnitDimensionDescription(UnitDimension value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public record SiUnitSlimDto(
    Guid Id,
    int Code,
    string Title,
    UnitDimension UnitDimension,
    string UnitDimensionTitle
);

public record SiUnitAsReferenceDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol,
    UnitDimension UnitDimension
);
