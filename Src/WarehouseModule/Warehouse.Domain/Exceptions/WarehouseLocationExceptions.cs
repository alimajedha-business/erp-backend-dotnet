using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class WarehouseLocationNotFoundException()
    : NotFoundException("WarehouseLocation");

public sealed class WarehouseLocationCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "WarehouseLocation.Code.Duplicate";
}

public sealed class WarehouseLocationParentNotInWarehouseException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "WarehouseLocation.Parent.NotInWarehouse";
}

public sealed class WarehouseLocationLastLevelCannotHaveChildrenException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "WarehouseLocation.LevelNo.LastLevelIf6";
}

public sealed class WarehouseLocationHasChildrenException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "WarehouseLocation.Delete.HasChildren";
}
