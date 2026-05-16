using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptLineContextService(
    IItemRepository itemRepository,
    IReceiptRepository receiptRepository
) : IReceiptLineContextService
{
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IReceiptRepository _receiptRepository = receiptRepository;

    public async Task<ReceiptLineItemContextDto> GetAsync(
        Guid companyId,
        Guid itemId,
        ReceiptLineItemContextRequestDto requestDto,
        CancellationToken ct
    )
    {
        var item = await _itemRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == itemId,
            trackChanges: false,
            ct
        ) ?? throw new ItemNotFoundException();

        var locations = item.ItemWarehouses
            .OrderBy(e => e.Warehouse.Title)
            .SelectMany(e => e.ItemWarehouseLocations
                .OrderBy(location => location.WarehouseLocation.Code)
                .Select(location => new ItemLocationContext(
                    WarehouseLocationId: location.WarehouseLocationId,
                    Title: location.WarehouseLocation.Title,
                    Code: location.WarehouseLocation.Code,
                    WarehouseId: e.WarehouseId,
                    WarehouseTitle: e.Warehouse.Title,
                    CanStoreItem: location.WarehouseLocation.CanStoreItem,
                    MaxMass: location.WarehouseLocation.MaxMass,
                    MaxVolume: location.WarehouseLocation.MaxVolume,
                    PreferredMassUnitId: location.WarehouseLocation.PreferredMassUnitId,
                    PreferredVolumeUnitId: location.WarehouseLocation.PreferredVolumeUnitId
                )))
            .ToList();

        var locationIds = locations
            .Select(e => e.WarehouseLocationId)
            .ToHashSet();

        var persistedOccupancies = await _receiptRepository.GetLocationOccupanciesAsync(
            companyId,
            locationIds,
            requestDto.CurrentReceiptId,
            ct
        );

        var occupancyByLocation = persistedOccupancies
            .ToDictionary(
                e => e.WarehouseLocationId,
                e => (e.OccupiedMass, e.OccupiedVolume)
            );

        foreach (var reservation in requestDto.CurrentReceiptLines)
        {
            if (!locationIds.Contains(reservation.WarehouseLocationId))
                continue;

            var existing = occupancyByLocation.GetValueOrDefault(reservation.WarehouseLocationId);
            occupancyByLocation[reservation.WarehouseLocationId] = (
                existing.OccupiedMass + (reservation.OccupiedMass ?? 0),
                existing.OccupiedVolume + (reservation.OccupiedVolume ?? 0)
            );
        }

        return new ReceiptLineItemContextDto(
            ItemId: item.Id,
            ItemCode: item.Code,
            ItemTitle: item.Title,
            UnitWeight: item.Weight,
            UnitVolume: item.Volume,
            PreferredMassUnitId: item.PreferredMassUnitId,
            PreferredVolumeUnitId: item.PreferredVolumeUnitId,
            UnitOfMeasurements: item.ItemUnitOfMeasurements
                .OrderBy(e => e.UnitOrder)
                .Select(e => new ReceiptLineItemUnitOfMeasurementContextDto(
                    ItemUnitOfMeasurementId: e.Id,
                    UnitOfMeasurementId: e.UnitOfMeasurementId,
                    Title: e.UnitOfMeasurement.Title,
                    Symbol: e.UnitOfMeasurement.Symbol,
                    UnitOrder: e.UnitOrder,
                    IsPrimary: e.UnitOrder == 1
                ))
                .ToList(),
            Locations: locations
                .Select(location =>
                {
                    var occupancy = occupancyByLocation.GetValueOrDefault(
                        location.WarehouseLocationId
                    );

                    return new ReceiptLineItemLocationContextDto(
                        WarehouseLocationId: location.WarehouseLocationId,
                        Title: location.Title,
                        Code: location.Code,
                        WarehouseId: location.WarehouseId,
                        WarehouseTitle: location.WarehouseTitle,
                        CanStoreItem: location.CanStoreItem,
                        MaxMass: location.MaxMass,
                        MaxVolume: location.MaxVolume,
                        PreferredMassUnitId: location.PreferredMassUnitId,
                        PreferredVolumeUnitId: location.PreferredVolumeUnitId,
                        OccupiedMass: occupancy.OccupiedMass,
                        OccupiedVolume: occupancy.OccupiedVolume,
                        AvailableMass: location.MaxMass.HasValue
                            ? location.MaxMass.Value - occupancy.OccupiedMass
                            : null,
                        AvailableVolume: location.MaxVolume.HasValue
                            ? location.MaxVolume.Value - occupancy.OccupiedVolume
                            : null
                    );
                })
                .ToList(),
            Attributes: item.ItemAttributes
                .OrderBy(e => e.Attribute.Code)
                .Select(e => new ReceiptLineItemAttributeContextDto(
                    ItemAttributeId: e.Id,
                    AttributeId: e.AttributeId,
                    Code: e.Attribute.Code,
                    Title: e.Attribute.Title,
                    DataType: e.Attribute.DataType,
                    IsRequired: e.Attribute.IsRequired,
                    IsStockDimension: e.Attribute.IsStockDimension,
                    IsStatic: e.Attribute.IsStatic
                ))
                .ToList()
        );
    }

    private record ItemLocationContext(
        Guid WarehouseLocationId,
        string Title,
        int Code,
        Guid WarehouseId,
        string WarehouseTitle,
        bool CanStoreItem,
        decimal? MaxMass,
        decimal? MaxVolume,
        Guid? PreferredMassUnitId,
        Guid? PreferredVolumeUnitId
    );
}
