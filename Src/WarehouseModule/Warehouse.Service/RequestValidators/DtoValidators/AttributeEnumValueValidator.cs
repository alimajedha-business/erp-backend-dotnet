using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.RequestValidators.DtoValidators;

public class CreateAttributeEnumValueValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<CreateAttributeEnumValueDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}

public class UpdateAttributeEnumValueValidator(
    IStringLocalizer<WarehouseResource> localizer
) : AbstractValidator<PatchAttributeEnumValueDto>
{
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
}