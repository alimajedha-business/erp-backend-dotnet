namespace NGErp.Base.Domain.Exceptions;

public class InvalidUserBadRequestException : BadRequestException
{
    public override string LocalizationKey => "Invalid.User";

    public InvalidUserBadRequestException() : base() { }
}
