namespace NGErp.Base.Domain.Exceptions;

public sealed class DbUpdateBadRequestException(
    string entityName,
    string reason
) : BadRequestException(entityName, reason)
{
    public override string LocalizationKey => "DbUpdateError";
}
