namespace NGErp.Base.Domain.Exceptions;

public sealed class InvalidOrderingException(string orderBy)
    : BadRequestException(orderBy)
{
    public string OrderBy { get; } = orderBy;
    public override string LocalizationKey => "Ordering.Invalid";
}
