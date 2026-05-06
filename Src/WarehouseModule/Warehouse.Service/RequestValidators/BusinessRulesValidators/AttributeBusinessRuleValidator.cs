using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class AttributeBusinessRuleValidator(
    IAttributeRepository attributeRepository,
    IAttributeEnumValueRepository enumValueRepository,
    ICategoryAttributeRuleRepository categoryAttributeRepository
) : IAttributeBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "title"
    };

    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IAttributeEnumValueRepository _enumRepository = enumValueRepository;
    private readonly ICategoryAttributeRuleRepository _categoryAttributeRepository = 
        categoryAttributeRepository;

    public void ValidateParameters(AttributeParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateAttributeDto createDto,
        CancellationToken ct
    )
    {
        await ValidateAttributeCodeUniquenessAsync(
            companyId,
            excludedAttributeId: null,
            createDto.Code,
            ct
        );
    }

    public async Task ValidateAttributeCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedAttributeId,
        int code,
        CancellationToken ct
    )
    {
        var exists = excludedAttributeId is null
            ? await _attributeRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Code == code,
                ct
            )
            : await _attributeRepository.AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.Id != excludedAttributeId.Value &&
                    e.Code == code,
                ct
            );

        if (exists)
            throw new AttributeCodeAlreadyExistsException(code);
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var attribute = await _attributeRepository.SingleOrDefaultAsync(
            predicate: e =>
                e.CompanyId == companyId &&
                e.Id == id,
            trackChanges: false,
            ct
        ) ?? throw new AttributeNotFoundException();

        if (attribute.IsStatic)
            throw new StaticAttributeCannotBeDeletedException();

        if (await HasEnumValuesAsync(id, ct))
            throw new AttributeHasEnumValuesException();

        if (await HasCategoryAttributeRulesAsync(id, ct))
            throw new AttributeHasCategoryAttributeRuleException();
    }

    public async Task ValidateDataTypeChangeAsync(
        Guid companyId,
        Guid id,
        AttributeDataType currentDataType,
        AttributeDataType newDataType,
        CancellationToken ct
    )
    {
        if (
            currentDataType != AttributeDataType.Enum ||
            newDataType == AttributeDataType.Enum
        )
        {
            return;
        }

        var exists = await _attributeRepository.AnyAsync(
            e => e.CompanyId == companyId && e.Id == id,
            ct
        );

        if (!exists)
            throw new AttributeNotFoundException();

        if (await HasEnumValuesAsync(id, ct))
            throw new AttributeDataTypeCannotChangeFromEnumException();
    }

    private Task<bool> HasEnumValuesAsync(
        Guid attributeId,
        CancellationToken ct
    )
    {
        return _enumRepository.AnyAsync(
            e => e.AttributeId == attributeId,
            ct
        );
    }

    private Task<bool> HasCategoryAttributeRulesAsync(
        Guid attributeId,
        CancellationToken ct
    )
    {
        return _categoryAttributeRepository.AnyAsync(
            e => e.AttributeId == attributeId,
            ct
        );
    }
}
