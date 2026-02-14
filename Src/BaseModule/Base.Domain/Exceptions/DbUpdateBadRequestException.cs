namespace NGErp.Base.Domain.Exceptions;

public class DbUpdateBadRequestException(string message = "Invalid Form Data.") : 
    BadRequestException(message)
{
}
