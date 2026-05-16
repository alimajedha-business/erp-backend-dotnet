using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptLineContextService(
    IItemRepository itemRepository
) : IReceiptLineContextService
{
    private readonly IItemRepository _itemRepository = itemRepository;

    public async Task<ReceiptLineItemContextDto> GetAsync(
        Guid companyId,
        Guid itemId,
        CancellationToken ct
    )
    {
        var item = await _itemRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == itemId,
            trackChanges: false,
            ct
        ) ?? throw new ItemNotFoundException();

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
            Locations: item.ItemWarehouses
                .OrderBy(e => e.Warehouse.Title)
                .SelectMany(e => e.ItemWarehouseLocations
                    .OrderBy(location => location.WarehouseLocation.Code)
                    .Select(location => new ReceiptLineItemLocationContextDto(
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
}
