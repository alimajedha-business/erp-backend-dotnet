namespace NGErp.Base.Domain.Exceptions;

public class InvalidPatchDocumentException(IEnumerable<string> errors) : 
    BadRequestException(string.Join(Environment.NewLine, errors))
{
    public IReadOnlyList<string> Errors { get; } = errors.ToList().AsReadOnly();

    public InvalidPatchDocumentException(params string[] errors)
        : this((IEnumerable<string>)errors)
    {
    }
}
