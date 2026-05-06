using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class AttributeNotFoundException()
    : NotFoundException("Attribute");

public sealed class AttributeCodeAlreadyExistsException(int code)
    : DuplicateResourceException(code)
{
    public int Code { get; } = code;
    public override string LocalizationKey => "Attribute.Code.Duplicate";
}

public sealed class AttributeHasEnumValuesException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "Attribute.Delete.HasEnumValues";
}

public sealed class StaticAttributeCannotBeDeletedException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "Attribute.Delete.IsStatic";
}

public sealed class AttributeDataTypeCannotChangeFromEnumException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "Attribute.DataType.EnumHasValues";
}

public sealed class AttributeHasCategoryAttributeRuleException()
    : BusinessRuleViolationException()
{
    public override string LocalizationKey => "Attribute.Delete.CategoryAttributeRule";
}
