namespace NGErp.Base.Domain.Exceptions;

public sealed class CollectionByIdsBadRequestException : BadRequestException
{
    public override string LocalizationKey => "Collection.CountMismatch";

    public CollectionByIdsBadRequestException() : base() { }
}
