using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class InventoryProjectionRepository(MainDbContext context) : IInventoryProjectionRepository
{
    private readonly MainDbContext _context = context;

    public async Task RebuildReceiptAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    )
    {
        await RemoveReceiptAsync(companyId, receiptId, ct);

        var receipt = await _context.Set<Receipt>()
            .AsSplitQuery()
            .Include(e => e.ReceiptType)

            .Include(e => e.ReceiptLines)
                .ThenInclude(e => e.WarehouseLocation)

            .Include(e => e.ReceiptLines)
                .ThenInclude(e => e.Item)
                    .ThenInclude(e => e.ItemUnitOfMeasurements)

            .Include(e => e.ReceiptLines)
                .ThenInclude(e => e.Item)
                    .ThenInclude(e => e.ItemAttributes)
                        .ThenInclude(e => e.Attribute)

            .Include(e => e.ReceiptLines)
                .ThenInclude(e => e.ReceiptLineMeasurementValues)

            .Include(e => e.ReceiptLines)
                .ThenInclude(e => e.ReceiptLineAttributeValues)
                    .ThenInclude(e => e.ItemAttribute)
                        .ThenInclude(e => e.Attribute)

            .SingleOrDefaultAsync(
                e => e.CompanyId == companyId && e.Id == receiptId,
                ct
            );

        if (receipt is null || receipt.Status == ReceiptStatus.Draft)
            return;

        foreach (var line in receipt.ReceiptLines)
        {
            var lot = await GetOrCreateLotAsync(companyId, line, ct);
            var quantity = GetPrimaryQuantity(line);
            var mass = GetLineMass(line, quantity);
            var volume = GetLineVolume(line, quantity);
            var direction = receipt.ReceiptType.AddToStock
                ? InventoryMovementDirection.Inbound
                : InventoryMovementDirection.Outbound;

            // TODO: add movement type reference
            var movement = new InventoryMovement
            {
                CompanyId = companyId,
                SourceDocumentId = receipt.Id,
                SourceDocumentLineId = line.Id,
                MovementDate = receipt.ReceiptDate.ToDateTime(TimeOnly.MinValue),
                Quantity = quantity,
                Mass = mass,
                Volume = volume,
                Direction = direction,
                Lot = lot,
                ToLocationId = direction == InventoryMovementDirection.Inbound
                    ? line.WarehouseLocationId
                    : null,
                FromLocationId = direction == InventoryMovementDirection.Outbound
                    ? line.WarehouseLocationId
                    : null
            };

            await _context.Set<InventoryMovement>().AddAsync(movement, ct);

            var sign = direction == InventoryMovementDirection.Inbound ? 1 : -1;
            await ApplyBalanceDeltaAsync(
                companyId,
                lot.Id,
                line.WarehouseLocationId,
                sign * quantity,
                sign * mass,
                sign * volume,
                ct
            );

            await ApplyInventoryBalanceDeltaAsync(
                companyId,
                line.ItemId,
                line.WarehouseLocation.WarehouseId,
                line.WarehouseLocationId,
                lot.Id,
                sign * quantity,
                sign * mass,
                sign * volume,
                ct
            );

            await ApplyLocationUsageDeltaAsync(
                line.WarehouseLocationId,
                sign * mass,
                sign * volume,
                ct
            );
        }

        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveReceiptAsync(
        Guid companyId,
        Guid receiptId,
        CancellationToken ct
    )
    {
        var movements = await _context.Set<InventoryMovement>()
            .Include(e => e.ToLocation)
            .Include(e => e.FromLocation)
            .Where(e => e.CompanyId == companyId && e.SourceDocumentId == receiptId)
            .ToListAsync(ct);

        foreach (var movement in movements)
        {
            var sign = movement.Direction == InventoryMovementDirection.Inbound ? -1 : 1;
            var locationId = movement.Direction == InventoryMovementDirection.Inbound
                ? movement.ToLocationId
                : movement.FromLocationId;
            var warehouseId = movement.Direction == InventoryMovementDirection.Inbound
                ? movement.ToLocation?.WarehouseId
                : movement.FromLocation?.WarehouseId;

            if (locationId.HasValue && warehouseId.HasValue)
            {
                await ApplyBalanceDeltaAsync(
                    companyId,
                    movement.LotId,
                    locationId.Value,
                    sign * movement.Quantity,
                    sign * movement.Mass,
                    sign * movement.Volume,
                    ct
                );

                var itemId = await _context.Set<InventoryLot>()
                    .Where(e => e.Id == movement.LotId)
                    .Select(e => e.ItemId)
                    .SingleAsync(ct);

                await ApplyInventoryBalanceDeltaAsync(
                    companyId,
                    itemId,
                    warehouseId.Value,
                    locationId.Value,
                    movement.LotId,
                    sign * movement.Quantity,
                    sign * movement.Mass,
                    sign * movement.Volume,
                    ct
                );

                await ApplyLocationUsageDeltaAsync(
                    locationId.Value,
                    sign * movement.Mass,
                    sign * movement.Volume,
                    ct
                );
            }
        }

        _context.Set<InventoryMovement>().RemoveRange(movements);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<WarehouseLocationUsage>> GetLocationUsagesAsync(
        Guid companyId,
        IReadOnlyCollection<Guid> warehouseLocationIds,
        CancellationToken ct
    )
    {
        if (warehouseLocationIds.Count == 0)
            return [];

        return await _context.Set<WarehouseLocationUsage>()
            .AsNoTracking()
            .Where(e => warehouseLocationIds.Contains(e.WarehouseLocationId))
            .ToListAsync(ct);
    }

    private async Task<InventoryLot> GetOrCreateLotAsync(
        Guid companyId,
        ReceiptLine line,
        CancellationToken ct
    )
    {
        var stockDimensions = GetStockDimensionValues(line);
        var stockKeyHash = ComputeStockKeyHash(stockDimensions);

        var candidateLots = await _context.Set<InventoryLot>()
            .Include(e => e.AttributeValues)
            .Where(e =>
                e.CompanyId == companyId &&
                e.ItemId == line.ItemId
            )
            .ToListAsync(ct);

        var existingLot = candidateLots.FirstOrDefault(e =>
            e.StockKeyHash.SequenceEqual(stockKeyHash)
        );

        if (existingLot is not null)
            return existingLot;

        var lot = new InventoryLot
        {
            Id = Guid.NewGuid(),
            CompanyId = companyId,
            ItemId = line.ItemId,
            StockKeyHash = stockKeyHash
        };

        foreach (var dimension in stockDimensions)
        {
            lot.AttributeValues.Add(new InventoryLotAttributeValue
            {
                CompanyId = companyId,
                AttributeId = dimension.AttributeId,
                StringValue = dimension.StringValue,
                DecimalValue = dimension.DecimalValue,
                DateTimeValue = dimension.DateTimeValue,
                BooleanValue = dimension.BooleanValue,
                EnumReferenceId = dimension.EnumReferenceId
            });
        }

        await _context.Set<InventoryLot>().AddAsync(lot, ct);
        return lot;
    }

    private static decimal GetPrimaryQuantity(ReceiptLine line)
    {
        var primaryUomId = line.Item.ItemUnitOfMeasurements
            .OrderBy(e => e.UnitOrder)
            .FirstOrDefault()
            ?.Id;

        if (primaryUomId.HasValue)
        {
            var primaryMeasurement = line.ReceiptLineMeasurementValues
                .FirstOrDefault(e => e.ItemUnitOfMeasurementId == primaryUomId.Value);

            if (primaryMeasurement is not null)
                return primaryMeasurement.Quantity;
        }

        return line.ReceiptLineMeasurementValues.FirstOrDefault()?.Quantity ?? 1;
    }

    private static decimal GetLineMass(ReceiptLine line, decimal primaryQuantity)
    {
        return line.Weight ?? (line.Item.Weight ?? 0) * primaryQuantity;
    }

    private static decimal GetLineVolume(ReceiptLine line, decimal primaryQuantity)
    {
        return line.Volume ?? (line.Item.Volume ?? 0) * primaryQuantity;
    }

    private async Task ApplyBalanceDeltaAsync(
        Guid companyId,
        Guid lotId,
        Guid warehouseLocationId,
        decimal quantityDelta,
        decimal massDelta,
        decimal volumeDelta,
        CancellationToken ct
    )
    {
        var balance = await _context.Set<InventoryLotLocationBalance>()
            .SingleOrDefaultAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.LotId == lotId &&
                    e.WarehouseLocationId == warehouseLocationId,
                ct
            );

        if (balance is null)
        {
            balance = new InventoryLotLocationBalance
            {
                CompanyId = companyId,
                LotId = lotId,
                WarehouseLocationId = warehouseLocationId
            };

            await _context.Set<InventoryLotLocationBalance>().AddAsync(balance, ct);
        }

        balance.Quantity += quantityDelta;
        balance.Mass += massDelta;
        balance.Volume += volumeDelta;
    }

    private async Task ApplyInventoryBalanceDeltaAsync(
        Guid companyId,
        Guid itemId,
        Guid warehouseId,
        Guid warehouseLocationId,
        Guid inventoryLotId,
        decimal quantityDelta,
        decimal massDelta,
        decimal volumeDelta,
        CancellationToken ct
    )
    {
        var balance = await _context.Set<InventoryBalance>()
            .SingleOrDefaultAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.ItemId == itemId &&
                    e.WarehouseId == warehouseId &&
                    e.WarehouseLocationId == warehouseLocationId &&
                    e.InventoryLotId == inventoryLotId,
                ct
            );

        if (balance is null)
        {
            balance = new InventoryBalance
            {
                CompanyId = companyId,
                ItemId = itemId,
                WarehouseId = warehouseId,
                WarehouseLocationId = warehouseLocationId,
                InventoryLotId = inventoryLotId
            };

            await _context.Set<InventoryBalance>().AddAsync(balance, ct);
        }

        balance.OnHandQuantity += quantityDelta;
        balance.OccupiedMass += massDelta;
        balance.OccupiedVolume += volumeDelta;
    }

    private async Task ApplyLocationUsageDeltaAsync(
        Guid warehouseLocationId,
        decimal massDelta,
        decimal volumeDelta,
        CancellationToken ct
    )
    {
        var usage = await _context.Set<WarehouseLocationUsage>()
            .SingleOrDefaultAsync(e => e.WarehouseLocationId == warehouseLocationId, ct);

        if (usage is null)
        {
            usage = new WarehouseLocationUsage
            {
                WarehouseLocationId = warehouseLocationId
            };

            await _context.Set<WarehouseLocationUsage>().AddAsync(usage, ct);
        }

        usage.OccupiedMass += massDelta;
        usage.OccupiedVolume += volumeDelta;
    }

    private static List<StockDimensionValue> GetStockDimensionValues(ReceiptLine line)
    {
        var values = new List<StockDimensionValue>();

        AddStaticStockDimensions(line, values);
        AddDynamicStockDimensions(line, values);

        return [.. values.OrderBy(e => e.Name)];
    }

    private static void AddStaticStockDimensions(
        ReceiptLine line,
        List<StockDimensionValue> values
    )
    {
        var staticItemAttributes = line.Item.ItemAttributes
            .Where(e =>
                e.Attribute.IsStatic &&
                e.Attribute.IsStockDimension)
            .ToList();

        foreach (var itemAttribute in staticItemAttributes)
        {
            var attribute = itemAttribute.Attribute;

            switch (attribute.StaticKey)
            {
                case StaticAttributeKeys.BatchNumber:
                    if (!string.IsNullOrWhiteSpace(line.BatchNumber))
                    {
                        values.Add(new StockDimensionValue(
                            Name: attribute.Code.ToString(),
                            AttributeId: attribute.Id,
                            StringValue: line.BatchNumber.Trim()
                        ));
                    }
                    break;

                case StaticAttributeKeys.SerialNumber:
                    if (!string.IsNullOrWhiteSpace(line.SerialNumber))
                    {
                        values.Add(new StockDimensionValue(
                            Name: attribute.Code.ToString(),
                            AttributeId: attribute.Id,
                            StringValue: line.SerialNumber.Trim()
                        ));
                    }
                    break;

                case StaticAttributeKeys.ExpiryDate:
                    if (line.ExpiryDate.HasValue)
                    {
                        values.Add(new StockDimensionValue(
                            Name: attribute.Code.ToString(),
                            AttributeId: attribute.Id,
                            DateTimeValue: line.ExpiryDate.Value
                        ));
                    }
                    break;
            }
        }
    }

    private static void AddDynamicStockDimensions(
    ReceiptLine line,
    List<StockDimensionValue> values
)
    {
        values.AddRange(line.ReceiptLineAttributeValues
            .Where(e =>
                !e.ItemAttribute.Attribute.IsStatic &&
                e.ItemAttribute.Attribute.IsStockDimension
            ).Select(e => new StockDimensionValue(
                Name: e.ItemAttribute.Attribute.Code.ToString(),
                AttributeId: e.ItemAttribute.AttributeId,
                StringValue: e.StringValue?.Trim(),
                DecimalValue: e.DecimalValue,
                BooleanValue: e.BooleanValue,
                EnumReferenceId: e.ReferenceId
            )));
    }

    private static byte[] ComputeStockKeyHash(IReadOnlyList<StockDimensionValue> dimensions)
    {
        var json = JsonSerializer.Serialize(dimensions.Select(e => new
        {
            e.Name,
            e.StringValue,
            e.DecimalValue,
            e.DateTimeValue,
            e.BooleanValue,
            e.EnumReferenceId
        }));

        return SHA256.HashData(Encoding.UTF8.GetBytes(json));
    }

    private static class StaticAttributeKeys
    {
        public const string BatchNumber = "sys_batch_number";
        public const string SerialNumber = "sys_serial_number";
        public const string ExpiryDate = "sys_expiry_date";
    }

    private sealed record StockDimensionValue(
        string Name,
        Guid AttributeId,
        string? StringValue = null,
        decimal? DecimalValue = null,
        DateTime? DateTimeValue = null,
        bool? BooleanValue = null,
        Guid? EnumReferenceId = null
    );
}
