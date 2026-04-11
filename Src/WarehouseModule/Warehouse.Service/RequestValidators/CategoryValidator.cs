using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.Service.RequestValidators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer;
    private readonly ICategoryLevelConstraintService _constraintService;

    public CreateCategoryValidator(
        IStringLocalizer<WarehouseResource> localizer,
        ICategoryLevelConstraintService constraintService
    )
    {
        _localizer = localizer;
        _constraintService = constraintService;

        RuleFor(p => p.Title)
            .NotEmpty()
            .WithMessage(_localizer["Category.Title.NotEmpty"].Value)
            .MinimumLength(3)
            .WithMessage(_localizer["Category.Title.MinLength"].Value);

        RuleFor(p => p.HasNextLevel)
            .Equal(true)
            .When(p => p.LevelNo == 1)
            .WithMessage(_localizer["Category.LevelNo.NotLastLevelIf1"].Value);

        RuleFor(p => p.HasNextLevel)
            .Equal(false)
            .When(p => p.LevelNo == 6)
            .WithMessage(_localizer["Category.LevelNo.LastLevelIf6"].Value);

        RuleFor(x => x.Code)
            .MustAsync(async (dto, code, ct) =>
            {
                var categoryLevel = await _constraintService.GetByLevelNo(
                    new Guid(), // TODO: get the company id
                    dto.LevelNo,
                    ct
                );

                if (categoryLevel == null)
                    return true;

                if (categoryLevel.CodeLength <= 0)
                    return true;

                return code.Length <= categoryLevel.CodeLength;
            })
            .WithMessage(_localizer["Category.Code.ExceedsMaxLength"].Value);
    }
}
