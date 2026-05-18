using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptLineContextService(
    IItemRepository itemRepository,
    IReceiptRepository receiptRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptLineContextService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
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
                    PreferredVolumeUnitId: location.WarehouseLocation.PreferredVolumeUnitId,
                    PreferredMassUnit: location.WarehouseLocation.PreferredMassUnit,
                    PreferredVolumeUnit: location.WarehouseLocation.PreferredVolumeUnit
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

            var (OccupiedMass, OccupiedVolume) = occupancyByLocation.GetValueOrDefault(reservation.WarehouseLocationId);
            var location = locations.First(e => e.WarehouseLocationId == reservation.WarehouseLocationId);
            occupancyByLocation[reservation.WarehouseLocationId] = (
                OccupiedMass + (
                    MeasurementUnitConverter.ConvertToBase(
                        reservation.OccupiedMass,
                        location.PreferredMassUnit
                    ) ?? 0
                ),
                OccupiedVolume + (
                    MeasurementUnitConverter.ConvertToBase(
                        reservation.OccupiedVolume,
                        location.PreferredVolumeUnit
                    ) ?? 0
                )
            );
        }

        return new ReceiptLineItemContextDto(
            ItemId: item.Id,
            ItemCode: item.Code,
            ItemTitle: item.Title,
            UnitWeight: ConvertFromBase(item.Weight, item.PreferredMassUnit),
            UnitVolume: ConvertFromBase(item.Volume, item.PreferredVolumeUnit),
            PreferredMassUnit: Localize(_mapper.Map<SiUnitAsReferenceDto>(item.PreferredMassUnit)),
            PreferredVolumeUnit: Localize(_mapper.Map<SiUnitAsReferenceDto>(item.PreferredVolumeUnit)),
            UnitOfMeasurements: [.. item.ItemUnitOfMeasurements
                .OrderBy(e => e.UnitOrder)
                .Select(e => new ReceiptLineItemUnitOfMeasurementContextDto(
                    ItemUnitOfMeasurementId: e.Id,
                    UnitOfMeasurementId: e.UnitOfMeasurementId,
                    Title: e.UnitOfMeasurement.Title,
                    Symbol: e.UnitOfMeasurement.Symbol,
                    UnitOrder: e.UnitOrder,
                    IsPrimary: e.UnitOrder == 1
                ))],
            Locations: [.. locations
                .Select(location =>
                {
                    var (OccupiedMass, OccupiedVolume) = occupancyByLocation.GetValueOrDefault(
                        location.WarehouseLocationId
                    );

                    return new ReceiptLineItemLocationContextDto(
                        WarehouseLocationId: location.WarehouseLocationId,
                        Title: location.Title,
                        Code: location.Code,
                        WarehouseId: location.WarehouseId,
                        WarehouseTitle: location.WarehouseTitle,
                        CanStoreItem: location.CanStoreItem,
                        MaxMass: MeasurementUnitConverter.ConvertFromBase(
                            location.MaxMass,
                            location.PreferredMassUnit
                        ),
                        MaxVolume: MeasurementUnitConverter.ConvertFromBase(
                            location.MaxVolume,
                            location.PreferredVolumeUnit
                        ),
                        PreferredMassUnitId: location.PreferredMassUnitId,
                        PreferredVolumeUnitId: location.PreferredVolumeUnitId,
                        OccupiedMass: MeasurementUnitConverter.ConvertFromBase(
                            OccupiedMass,
                            location.PreferredMassUnit
                        ) ?? OccupiedMass,
                        OccupiedVolume: MeasurementUnitConverter.ConvertFromBase(
                            OccupiedVolume,
                            location.PreferredVolumeUnit
                        ) ?? OccupiedVolume,
                        AvailableMass: location.MaxMass.HasValue
                            ? MeasurementUnitConverter.ConvertFromBase(
                                location.MaxMass.Value - OccupiedMass,
                                location.PreferredMassUnit
                            )
                            : null,
                        AvailableVolume: location.MaxVolume.HasValue
                            ? MeasurementUnitConverter.ConvertFromBase(
                                location.MaxVolume.Value - OccupiedVolume,
                                location.PreferredVolumeUnit
                            )
                            : null
                    );
                })],
            Attributes: [.. item.ItemAttributes
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
                ))]
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
        Guid? PreferredVolumeUnitId,
        SiUnit? PreferredMassUnit,
        SiUnit? PreferredVolumeUnit
    );

    private static decimal? ConvertFromBase(decimal? value, SiUnit? preferredUnit)
        => MeasurementUnitConverter.ConvertFromBase(value, preferredUnit);

    private SiUnitAsReferenceDto Localize(SiUnitAsReferenceDto dto)
    {
        return dto with { Title = _localizer[dto.Title].Value };
    }
}
