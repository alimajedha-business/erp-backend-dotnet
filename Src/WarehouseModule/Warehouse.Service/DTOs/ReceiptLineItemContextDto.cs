using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptLineItemContextDto(
    Guid ItemId,
    string ItemCode,
    string ItemTitle,
    decimal? UnitWeight,
    decimal? UnitVolume,
    Guid? PreferredMassUnitId,
    Guid? PreferredVolumeUnitId,
    IReadOnlyList<ReceiptLineItemUnitOfMeasurementContextDto> UnitOfMeasurements,
    IReadOnlyList<ReceiptLineItemLocationContextDto> Locations,
    IReadOnlyList<ReceiptLineItemAttributeContextDto> Attributes
);

public class ReceiptLineItemContextRequestDto
{
    public Guid? CurrentReceiptId { get; set; }
    public IReadOnlyList<ReceiptLineLocationReservationDto> CurrentReceiptLines { get; set; } = [];
}

public record ReceiptLineLocationReservationDto(
    int RowNumber,
    Guid WarehouseLocationId,
    decimal? OccupiedMass,
    decimal? OccupiedVolume
);

public record ReceiptLineItemUnitOfMeasurementContextDto(
    Guid ItemUnitOfMeasurementId,
    Guid UnitOfMeasurementId,
    string Title,
    string Symbol,
    int UnitOrder,
    bool IsPrimary
);

public record ReceiptLineItemLocationContextDto(
    Guid WarehouseLocationId,
    string Title,
    int Code,
    Guid WarehouseId,
    string WarehouseTitle,
    bool CanStoreItem,
    decimal? MaxMass,
    decimal? MaxVolume,
    Guid? PreferredMassUnitId,
    Guid? PreferredVolumeUnitId,
    decimal OccupiedMass,
    decimal OccupiedVolume,
    decimal? AvailableMass,
    decimal? AvailableVolume
);

public record ReceiptLineItemAttributeContextDto(
    Guid ItemAttributeId,
    Guid AttributeId,
    int Code,
    string Title,
    AttributeDataType DataType,
    bool IsRequired,
    bool IsStockDimension,
    bool IsStatic
);
