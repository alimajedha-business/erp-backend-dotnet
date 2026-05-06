using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class WarehouseNotFoundException()
    : NotFoundException("Warehouse");

public sealed class WarehouseCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "Warehouse.Code.Duplicate";
}

public sealed class WarehouseHasLocationsException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "Warehouse.Delete.HasLocations";
}
