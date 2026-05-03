using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class WarehouseTypeCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "WarehouseType.Code.Duplicate";
}

public sealed class WarehouseTypeHasWarehousesException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "WarehouseType.Delete.HasWarehouses";
}
