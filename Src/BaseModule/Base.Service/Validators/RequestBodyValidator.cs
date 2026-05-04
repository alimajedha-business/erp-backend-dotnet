using FluentValidation;
using FluentValidation.Results;

namespace NGErp.Base.Service.Validators;

public static class RequestBodyValidator
{
    public static void ThrowIfNull<T>(
        T? createDto
    )
    {
        if (createDto is not null)
            return;

        throw new ValidationException([
            new ValidationFailure()
        ]);
    }

    public static async Task ValidateAsync<T>(
        IValidator<T> validator,
        T dto,
        CancellationToken ct
    )
    {
        var validationResult = await validator.ValidateAsync(dto, ct);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
