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
            dto.Number,
            ct
        );

        await _receiptTypeService.GetSingleOrThrowAsync(
            trackChanges: false,
            predicate: e => e.CompanyId == companyId && e.Id == dto.ReceiptTypeId,
            ct
        );

        ValidateDuplicateHeaderValues(dto.ReceiptFieldValues);
        ValidateHeaderValueObjects(dto.ReceiptFieldValues);

        var configuration = await _configurationRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.ReceiptTypeId == dto.ReceiptTypeId,
            trackChanges: false,
            ct
        ) ?? throw new ReceiptTypeConfigurationNotFoundException();

        var configurations = configuration.FieldConfigurations
            .ToDictionary(e => e.FieldDefinitionId);

        var headerConfigurations = configurations.Values
            .Where(e => e.Exists && e.Placement == ReceiptConfiguredPlacement.Header)
            .ToDictionary(e => e.FieldDefinitionId);

        var detailConfigurations = configurations.Values
            .Where(e => e.Exists && e.Placement == ReceiptConfiguredPlacement.Detail)
            .ToDictionary(e => e.FieldDefinitionId);

        ValidateConfiguredFieldValues(
            dto.ReceiptFieldValues,
            configurations,
            ReceiptConfiguredPlacement.Header
        );
        ValidateRequiredFields(dto.ReceiptFieldValues, headerConfigurations);

        await ValidateReceiptLinesAsync(
            companyId,
            dto.ReceiptLines,
            configurations,
            detailConfigurations,
            ct
        );
    }

    private async Task ValidateNumberUniquenessAsync(
        Guid companyId,
        Guid? excludedReceiptId,
        long number,
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

    private async Task ValidateReceiptLinesAsync(
        Guid companyId,
        IReadOnlyCollection<CreateReceiptLineDto> lines,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> configurations,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> detailConfigurations,
        CancellationToken ct
    )
    {
        var errors = new Dictionary<int, List<Exception>>();

        AddDuplicateRowErrors(lines, errors);

        foreach (var line in lines)
        {
            AddLineDuplicateValueErrors(line, errors);
            AddLineValueObjectErrors(line, errors);
            AddLineConfiguredFieldErrors(line, configurations, detailConfigurations, errors);
        }

        await AddLineItemAndUnitErrorsAsync(companyId, lines, errors, ct);

        if (errors.Count > 0)
        {
            throw new ReceiptLineValidationException(
                errors.ToDictionary(
                    e => e.Key,
                    e => (IReadOnlyList<Exception>)e.Value
                )
            );
        }
    }

    private static void AddDuplicateRowErrors(
        IReadOnlyCollection<CreateReceiptLineDto> lines,
        Dictionary<int, List<Exception>> errors
    )
    {
        var duplicates = lines
            .GroupBy(e => e.RowNumber)
            .Where(e => e.Count() > 1);

        foreach (var duplicate in duplicates)
            AddLineError(errors, duplicate.Key, new ReceiptDuplicateRowNumberException(duplicate.Key));
    }

    private static void ValidateDuplicateHeaderValues(
        IReadOnlyCollection<CreateReceiptFieldValueDto> fieldValues
    )
    {
        var duplicateHeaderField = fieldValues
            .GroupBy(e => e.FieldDefinitionId)
            .FirstOrDefault(e => e.Count() > 1);

        if (duplicateHeaderField is not null)
            throw new ReceiptDuplicateFieldDefinitionException(duplicateHeaderField.Key);
    }

    private static void AddLineDuplicateValueErrors(
        CreateReceiptLineDto line,
        Dictionary<int, List<Exception>> errors
    )
    {
        var duplicateLineFields = line.ReceiptFieldValues
            .GroupBy(e => e.FieldDefinitionId)
            .Where(e => e.Count() > 1);

        foreach (var duplicate in duplicateLineFields)
            AddLineError(errors, line.RowNumber, new ReceiptDuplicateFieldDefinitionException(duplicate.Key));

        var duplicateAttributes = line.ReceiptLineAttributeValues
            .GroupBy(e => e.ItemAttributeId)
            .Where(e => e.Count() > 1);

        foreach (var duplicate in duplicateAttributes)
            AddLineError(errors, line.RowNumber, new ReceiptDuplicateLineAttributeException(duplicate.Key));

        var duplicateMeasurements = line.ReceiptLineMeasurementValues
            .GroupBy(e => e.ItemUnitOfMeasurementId)
            .Where(e => e.Count() > 1);

        foreach (var duplicate in duplicateMeasurements)
        {
            AddLineError(
                errors,
                line.RowNumber,
                new ReceiptDuplicateLineMeasurementValueException(duplicate.Key)
            );
        }
    }

    private static void ValidateHeaderValueObjects(
        IReadOnlyCollection<CreateReceiptFieldValueDto> fieldValues
    )
    {
        foreach (var headerValue in fieldValues)
            ValidateOnlyOneValueIsFilled(headerValue);
    }

    private static void AddLineValueObjectErrors(
        CreateReceiptLineDto line,
        Dictionary<int, List<Exception>> errors
    )
    {
        foreach (var attributeValue in line.ReceiptLineAttributeValues)
        {
            if (!HasExactlyOneValue(attributeValue))
            {
                AddLineError(
                    errors,
                    line.RowNumber,
                    new ReceiptLineAttributeValueMustHaveExactlyOneValueException(
                        attributeValue.ItemAttributeId
                    )
                );
            }
        }

        foreach (var fieldValue in line.ReceiptFieldValues)
        {
            if (!HasExactlyOneValue(fieldValue))
            {
                AddLineError(
                    errors,
                    line.RowNumber,
                    new ReceiptFieldValueMustHaveExactlyOneValueException(
                        fieldValue.FieldDefinitionId
                    )
                );
            }
        }
    }

    private static void AddLineConfiguredFieldErrors(
        CreateReceiptLineDto line,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> configurations,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> detailConfigurations,
        Dictionary<int, List<Exception>> errors
    )
    {
        foreach (var fieldValue in line.ReceiptFieldValues)
            AddConfiguredFieldValueErrorIfInvalid(
                fieldValue,
                configurations,
                ReceiptConfiguredPlacement.Detail,
                line.RowNumber,
                errors
            );

        var provided = line.ReceiptFieldValues
            .Select(e => e.FieldDefinitionId)
            .ToHashSet();

        var missingRequiredFields = detailConfigurations.Values
            .Where(e => e.IsRequired && !provided.Contains(e.FieldDefinitionId));

        foreach (var missing in missingRequiredFields)
        {
            AddLineError(
                errors,
                line.RowNumber,
                new ReceiptRequiredFieldValueMissingException(
                    missing.FieldDefinitionId,
                    missing.FieldDefinition.Title
                )
            );
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
                    fieldValue.FieldDefinitionId,
                    GetPlacementLocalizationKey(placement)
                );

            if (!configuration.Exists || configuration.Placement != placement)
                throw new ReceiptFieldDefinitionNotConfiguredException(
                    fieldValue.FieldDefinitionId,
                    configuration.FieldDefinition.Title,
                    GetPlacementLocalizationKey(placement)
                );

            ValidateFieldValueDataType(fieldValue, configuration.FieldDefinition);
        }
    }

    private static void AddConfiguredFieldValueErrorIfInvalid(
        CreateReceiptFieldValueDto fieldValue,
        IReadOnlyDictionary<Guid, ReceiptTypeFieldConfiguration> configurations,
        ReceiptConfiguredPlacement placement,
        int rowNumber,
        Dictionary<int, List<Exception>> errors
    )
    {
        if (!configurations.TryGetValue(fieldValue.FieldDefinitionId, out var configuration))
        {
            AddLineError(
                errors,
                rowNumber,
                new ReceiptFieldDefinitionNotConfiguredException(
                    fieldValue.FieldDefinitionId,
                    GetPlacementLocalizationKey(placement)
                )
            );
            return;
        }

        if (!configuration.Exists || configuration.Placement != placement)
        {
            AddLineError(
                errors,
                rowNumber,
                new ReceiptFieldDefinitionNotConfiguredException(
                    fieldValue.FieldDefinitionId,
                    configuration.FieldDefinition.Title,
                    GetPlacementLocalizationKey(placement)
                )
            );
            return;
        }

        if (!IsFieldValueDataTypeValid(fieldValue, configuration.FieldDefinition))
        {
            AddLineError(
                errors,
                rowNumber,
                new ReceiptFieldValueDataTypeMismatchException(
                    fieldValue.FieldDefinitionId,
                    configuration.FieldDefinition.Title,
                    GetDataTypeLocalizationKey(configuration.FieldDefinition.DataType)
                )
            );
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
            throw new ReceiptRequiredFieldValueMissingException(
                missing.FieldDefinitionId,
                missing.FieldDefinition.Title
            );
    }

    private static void ValidateFieldValueDataType(
        CreateReceiptFieldValueDto dto,
        ReceiptFieldDefinition definition
    )
    {
        if (!IsFieldValueDataTypeValid(dto, definition))
            throw new ReceiptFieldValueDataTypeMismatchException(
                dto.FieldDefinitionId,
                definition.Title,
                GetDataTypeLocalizationKey(definition.DataType)
            );
    }

    private static bool IsFieldValueDataTypeValid(
        CreateReceiptFieldValueDto dto,
        ReceiptFieldDefinition definition
    )
    {
        return definition.DataType switch
        {
            ReceiptFieldDataType.Text => dto.StringValue is not null,
            ReceiptFieldDataType.Integer => dto.IntValue is not null,
            ReceiptFieldDataType.Decimal => dto.DecimalValue is not null,
            ReceiptFieldDataType.Date => dto.DateValue is not null || dto.DateTimeValue is not null,
            ReceiptFieldDataType.Boolean => dto.BooleanValue is not null,
            ReceiptFieldDataType.Guid => dto.ReferenceId is not null,
            _ => false
        };
    }

    private static string GetPlacementLocalizationKey(ReceiptConfiguredPlacement placement) =>
        $"ReceiptConfiguredPlacement.{placement}";

    private static string GetDataTypeLocalizationKey(ReceiptFieldDataType dataType) =>
        $"ReceiptFieldDataType.{dataType}";

    private async Task AddLineItemAndUnitErrorsAsync(
        Guid companyId,
        IReadOnlyCollection<CreateReceiptLineDto> lines,
        Dictionary<int, List<Exception>> errors,
        CancellationToken ct
    )
    {
        var itemCache = new Dictionary<Guid, Item>();

        foreach (var line in lines)
        {
            if (!itemCache.TryGetValue(line.ItemId, out var item))
            {
                item = await _itemRepository.SingleOrDefaultAsync(
                    e => e.CompanyId == companyId && e.Id == line.ItemId,
                    trackChanges: false,
                    ct
                );

                if (item is null)
                {
                    AddLineError(errors, line.RowNumber, new ItemNotFoundException());
                    continue;
                }

                itemCache[line.ItemId] = item;
            }

            var itemUnitOfMeasurementIds = item.ItemUnitOfMeasurements
                .Select(e => e.Id)
                .ToHashSet();

            foreach (var measurementValue in line.ReceiptLineMeasurementValues)
            {
                var unitIsAllowed = itemUnitOfMeasurementIds.Contains(
                    measurementValue.ItemUnitOfMeasurementId
                );

                if (!unitIsAllowed)
                    AddLineError(
                        errors,
                        line.RowNumber,
                        new ReceiptLineUnitOfMeasurementNotAllowedException(
                            line.RowNumber,
                            measurementValue.ItemUnitOfMeasurementId
                        )
                    );
            }
        }
    }

    private static void AddLineError(
        Dictionary<int, List<Exception>> errors,
        int rowNumber,
        Exception exception
    )
    {
        if (!errors.TryGetValue(rowNumber, out var rowErrors))
        {
            rowErrors = [];
            errors[rowNumber] = rowErrors;
        }

        rowErrors.Add(exception);
    }

    private static void ValidateOnlyOneValueIsFilled(CreateReceiptFieldValueDto dto)
    {
        if (!HasExactlyOneValue(dto))
            throw new ReceiptFieldValueMustHaveExactlyOneValueException(dto.FieldDefinitionId);
    }

    private static bool HasExactlyOneValue(CreateReceiptFieldValueDto dto)
    {
        return new object?[]
        {
            dto.StringValue,
            dto.IntValue,
            dto.DecimalValue,
            dto.DateValue,
            dto.DateTimeValue,
            dto.ReferenceId,
            dto.BooleanValue
        }.Count(e => e is not null) == 1;
    }

    private static bool HasExactlyOneValue(CreateReceiptLineAttributeValueDto dto)
    {
        return new object?[]
        {
            dto.StringValue,
            dto.DecimalValue,
            dto.DateValue,
            dto.DateTimeValue,
            dto.ReferenceId,
            dto.BooleanValue
        }.Count(e => e is not null) == 1;
    }
}
