namespace NGErp.Base.Domain.Exceptions;

public sealed class InvalidPatchDocumentException(IEnumerable<string> errors)
    : BadRequestException(string.Join(Environment.NewLine, errors))
{
    public override string LocalizationKey => "PatchDocument.Invalid";

    public IReadOnlyList<string> Errors { get; } = errors.ToList().AsReadOnly();

    public InvalidPatchDocumentException(params string[] errors)
        : this((IEnumerable<string>)errors)
    {
    }
}