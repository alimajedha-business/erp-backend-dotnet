using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class FeatureSettingsNotFoundException()
    : NotFoundException("FeatureSettings");

public sealed class FeatureSettingsAlreadyExistsException()
    : DuplicateResourceException()
{
    public override string LocalizationKey => "FeatureSettings.Duplicate";
}
