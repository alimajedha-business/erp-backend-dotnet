namespace NGErp.Base.Domain.Exceptions;

public abstract class BadRequestException(string message) : Exception(message)
{
}
