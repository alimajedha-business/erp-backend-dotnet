using NGErp.Warehouse.Domain.Entities;

namespace NGErp.Warehouse.Service.DTOs;

public record ReceiptLineItemContextDto(
    Guid ItemId,
    string ItemCode,
    string ItemTitle,
    decimal? UnitWeight,
    decimal? UnitVolume,
    SiUnitAsReferenceDto? PreferredMassUnit,
    SiUnitAsReferenceDto? PreferredVolumeUnit,
    IReadOnlyList<ReceiptLineItemUnitOfMeasurementContextDto> UnitOfMeasurements,
    IReadOnlyList<ReceiptLineItemLocationContextDto> Locations,
    IReadOnlyList<ReceiptLineItemAttributeContextDto> Attributes
);

public class ReceiptLineItemContextRequestDto
{
    /* Current receipt is excluded when editing:
     * When currentReceiptId is provided, the query excludes that receipt’s saved lines. 
     * Then the frontend’s current dialog lines are added back as reserved occupancy.
     * This prevents double-counting while editing an existing receipt. */
    public Guid? CurrentReceiptId { get; set; }
    public IReadOnlyList<ReceiptLineLocationReservationDto> CurrentReceiptLines { get; set; } = [];

    /* example:
     *  {
          "currentReceiptId": "optional-existing-receipt-guid",
          "currentReceiptLines": [
            {
              "rowNumber": 1,
              "warehouseLocationId": "location-guid",
              "occupiedMass": 12.5,
              "occupiedVolume": 3.25
            }
          ]
        }
    */
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
