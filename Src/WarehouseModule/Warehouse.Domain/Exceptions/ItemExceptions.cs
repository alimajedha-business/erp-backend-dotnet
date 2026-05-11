using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ItemNotFoundException()
    : NotFoundException("Item");

public sealed class ItemCodeAlreadyExistsException(string code)
    : DuplicateResourceException(code)
{
    public string Code { get; } = code;
    public override string LocalizationKey => "Item.Code.Duplicate";
}

public sealed class ItemUnitOfMeasurementCountExceededException(int maxCount)
    : BusinessRuleViolationException(maxCount)
{
    public int MaxCount { get; } = maxCount;
    public override string LocalizationKey => "Item.ItemUnitOfMeasurements.MaxCount";
}
