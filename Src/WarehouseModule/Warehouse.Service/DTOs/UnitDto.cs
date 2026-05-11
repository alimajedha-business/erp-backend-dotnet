using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record UnitDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol,
    decimal FactorToBase,
    bool IsBaseUnit,
    UnitDimension UnitDimension
);

public record UnitSlimDto(
    Guid Id,
    int Code,
    string Title,
    string Symbol,
    UnitDimension UnitDimension
);
