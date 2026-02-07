namespace NGErp.Base.Domain.Exceptions;

public class InvalidUserBadRequestException : BadRequestException
{
    public InvalidUserBadRequestException() : base("Invalid user")
    {
    }
}
