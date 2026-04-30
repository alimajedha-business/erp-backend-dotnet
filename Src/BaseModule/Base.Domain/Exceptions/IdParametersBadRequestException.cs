namespace NGErp.Base.Domain.Exceptions;

public sealed class IdParametersBadRequestException : BadRequestException
{
    public override string LocalizationKey => "Id.Parameters";

    public IdParametersBadRequestException() : base() { }
}
