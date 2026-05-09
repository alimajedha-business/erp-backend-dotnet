using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ReceiptBusinessRuleValidator(
    IReceiptRepository receiptRepository,
    IItemRepository itemRepository,
    IReceiptTypeConfigurationRepository configurationRepository,
    IReceiptTypeService receiptTypeService
) : IReceiptBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "number",
        "receiptDate"
    };

    private readonly IReceiptRepository _receiptRepository = receiptRepository;
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IReceiptTypeConfigurationRepository _configurationRepository =
        configurationRepository;
    private readonly IReceiptTypeService _receiptTypeService = receiptTypeService;

    public void ValidateParameters(ReceiptParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptDto createDto,
        CancellationToken ct
    )
    {
        await ValidateReceiptAsync(
            companyId,
            excludedReceiptId: null,
            createDto,
            ct
        );
    }

    public async Task ValidateUpdateAsync(
        Guid companyId,
        Guid id,
        CreateReceiptDto updateDto,
        CancellationToken ct
    )
    {
        await ValidateReceiptAsync(
            companyId,
            excludedReceiptId: id,
            updateDto,
            ct
        );
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _receiptRepository.AnyAsync(
            e => e.CompanyId == companyId && e.Id == id,
            ct
        );

        if (!exists)
            throw new ReceiptNotFoundException();
    }

    private async Task ValidateReceiptAsync(
        Guid companyId,
        Guid? excludedReceiptId,
        CreateReceiptDto dto,
        CancellationToken ct
    )
    {
        await ValidateNumberUniquenessAsync(
            companyId,
            excludedReceiptId,
            dto.Number.Trim(),
            ct
        );

        await _receiptTypeService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: e => e.CompanyId == companyId && e.Id == dto.ReceiptTypeId,
            ct
        );

        ValidateDuplicateRows(dto);
        ValidateDuplicateValues(dto);
        ValidateValueObjects(dto);
        await ValidateConfiguredFieldsAsync(companyId, dto, ct);
        await ValidateItemsAndUnitsAsync(companyId, dto, ct);
    }

    private async Task ValidateNumberUniquenessAsync(
        Guid companyId,
        Guid? excludedReceiptId,
        string number,
        CancellationToken ct
    )
    {
        var exists = excludedReceiptId is null
            ? await _receiptRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Number == number,
                ct
            )
            : await _receiptRepository.AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.Id != excludedReceiptId.Value &&
                    e.Number == number,
                ct
            );

        if (exists)
            throw new ReceiptNumberAlreadyExistsException(number);
    }

    private static void ValidateDuplicateRows(CreateReceiptDto createDto)
    {
        var duplicate = createDto.ReceiptLines
            .GroupBy(e => e.RowNumber)
            .FirstOrDefault(e => e.Count() > 1);

        if (duplicate is not null)
            throw new ReceiptDuplicateRowNumberException(duplicate.Key);
    }

    private static void ValidateDuplicateValues(CreateReceiptDto createDto)
    {
        var duplicateHeaderField = createDto.ReceiptFieldValues
            .GroupBy(e => e.FieldDefinitionId)
            .FirstOrDefault(e => e.Count() > 1);

        if (duplicateHeaderField is not null)
            throw new ReceiptDuplicateFieldDefinitionException(duplicateHeaderField.Key);

        foreach (var line in createDto.ReceiptLines)
        {
            var duplicateLineField = line.ReceiptFieldValues
                .GroupBy(e => e.FieldDefinitionId)
                .FirstOrDefault(e => e.Count() > 1);

            if (duplicateLineField is not null)
                throw new ReceiptDuplicateFieldDefinitionException(duplicateLineField.Key);

            var duplicateAttribute = line.ReceiptLineAttributeValues
                .GroupBy(e => e.ItemAttributeId)
                .FirstOrDefault(e => e.Count() > 1);

            if (duplicateAttribute is not null)
                throw new ReceiptDuplicateLineAttributeException(duplicateAttribute.Key);
        }
    }

    private static void ValidateValueObjects(CreateReceiptDto createDto)
    {
        foreach (var headerValue in createDto.ReceiptFieldValues)
            ValidateOnlyOneValueIsFilled(headerValue);

        foreach (var line in createDto.ReceiptLines)
        {
            foreach (var attributeValue in line.ReceiptLineAttributeValues)
                ValidateOnlyOneValueIsFilled(attributeValue);

            foreach (var fieldValue in line.ReceiptFieldValues)
                ValidateOnlyOneValueIsFilled(fieldValue);
        }
    }

    private async Task ValidateConfiguredFieldsAsync(
        Guid companyId,
        CreateReceiptDto dto,
        CancellationToken ct
    )
    {
        var configuration = await _configurationRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.ReceiptTypeId == dto.ReceiptTypeId,
            trackChanges: false,
            ct
        ) ?? throw new ReceiptTypeConfigurationNotFoundException();

        var headerConfigurations = configuration.FieldConfigurations
            .Where(e => e.Exists && e.Placement == ReceiptConfiguredPlacement.Header)
            .ToDictionary(e => e.FieldDefinitionId);

        var detailConfigurations = configuration.FieldConfigurations
            .Where(e => e.Exists && e.Placement == ReceiptConfiguredPlacement.Detail)
            .ToDictionary(e => e.FieldDefinitionId);

        ValidateConfiguredFieldValues(
            dto.ReceiptFieldValues,
            headerConfigurations,
            ReceiptConfiguredPlacement.Header
        );
        ValidateRequiredFields(dto.ReceiptFieldValues, headerConfigurations);

        foreach (var line in dto.ReceiptLines)
        {
            ValidateConfiguredFieldValues(
                line.ReceiptFieldValues,
                detailConfigurations,
                ReceiptConfiguredPlacement.Detail
            );
            ValidateRequiredFields(line.ReceiptFieldValues, detailConfigurations);
        }
    }

    private static void ValidateConfiguredFieldValues(
        IEnumerable<CreateReceiptFieldValueDto> fieldValues,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> configurations,
        ReceiptConfiguredPlacement placement
    )
    {
        foreach (var fieldValue in fieldValues)
        {
            if (!configurations.TryGetValue(fieldValue.FieldDefinitionId, out var configuration))
                throw new ReceiptFieldDefinitionNotConfiguredException(
                    fieldValue.FieldDefinitionId
                );

            if (configuration.Placement != placement)
                throw new ReceiptFieldDefinitionNotConfiguredException(
                    fieldValue.FieldDefinitionId
                );

            ValidateFieldValueDataType(fieldValue, configuration.FieldDefinition);
        }
    }

    private static void ValidateRequiredFields(
        IEnumerable<CreateReceiptFieldValueDto> fieldValues,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> configurations
    )
    {
        var provided = fieldValues
            .Select(e => e.FieldDefinitionId)
            .ToHashSet();

        var missing = configurations.Values
            .Where(e => e.IsRequired)
            .FirstOrDefault(e => !provided.Contains(e.FieldDefinitionId));

        if (missing is not null)
            throw new ReceiptRequiredFieldValueMissingException(missing.FieldDefinitionId);
    }

    private static void ValidateFieldValueDataType(
        CreateReceiptFieldValueDto dto,
        ReceiptFieldDefinition definition
    )
    {
        var isValid = definition.DataType switch
        {
            ReceiptFieldDataType.Text => dto.StringValue is not null,
            ReceiptFieldDataType.Integer => dto.IntValue is not null,
            ReceiptFieldDataType.Decimal => dto.DecimalValue is not null,
            ReceiptFieldDataType.Date => dto.DateValue is not null || dto.DateTimeValue is not null,
            ReceiptFieldDataType.Boolean => dto.BooleanValue is not null,
            ReceiptFieldDataType.Guid => dto.ReferenceId is not null,
            _ => false
        };

        if (!isValid)
            throw new ReceiptFieldValueDataTypeMismatchException(dto.FieldDefinitionId);
    }

    private async Task ValidateItemsAndUnitsAsync(
        Guid companyId,
        CreateReceiptDto dto,
        CancellationToken ct
    )
    {
        var itemCache = new Dictionary<Guid, Item>();

        foreach (var line in dto.ReceiptLines)
        {
            if (!itemCache.TryGetValue(line.ItemId, out var item))
            {
                item = await _itemRepository.SingleOrDefaultAsync(
                    e => e.CompanyId == companyId && e.Id == line.ItemId,
                    trackChanges: false,
                    ct
                ) ?? throw new ItemNotFoundException();

                itemCache[line.ItemId] = item;
            }

            var unitIsAllowed =
                item.PrimaryUnitOfMeasurementId == line.UnitOfMeasurementId ||
                item.ItemUnitOfMeasurements.Any(e =>
                    e.UnitOfMeasurementId == line.UnitOfMeasurementId
                );

            if (!unitIsAllowed)
                throw new ReceiptLineUnitOfMeasurementNotAllowedException(
                    line.UnitOfMeasurementId
                );
        }
    }

    private static void ValidateOnlyOneValueIsFilled(CreateReceiptFieldValueDto dto)
    {
        var filledCount = new object?[]
        {
            dto.StringValue,
            dto.IntValue,
            dto.DecimalValue,
            dto.DateValue,
            dto.DateTimeValue,
            dto.ReferenceId,
            dto.BooleanValue
        }.Count(e => e is not null);

        if (filledCount != 1)
            throw new ReceiptFieldValueMustHaveExactlyOneValueException(dto.FieldDefinitionId);
    }

    private static void ValidateOnlyOneValueIsFilled(CreateReceiptLineAttributeValueDto dto)
    {
        var filledCount = new object?[]
        {
            dto.StringValue,
            dto.DecimalValue,
            dto.DateValue,
            dto.DateTimeValue,
            dto.ReferenceId,
            dto.BooleanValue
        }.Count(e => e is not null);

        if (filledCount != 1)
            throw new ReceiptLineAttributeValueMustHaveExactlyOneValueException(
                dto.ItemAttributeId
            );
    }
}
