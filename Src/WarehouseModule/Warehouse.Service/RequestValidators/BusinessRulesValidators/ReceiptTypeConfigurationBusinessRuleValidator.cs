using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ReceiptTypeConfigurationBusinessRuleValidator(
    IReceiptTypeRepository receiptTypeRepository,
    IReceiptFieldDefinitionRepository fieldDefinitionRepository,
    IReceiptTypeConfigurationRepository configurationRepository
) : IReceiptTypeConfigurationBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "receiptTypeId"
    };

    private readonly IReceiptTypeRepository _receiptTypeRepository = receiptTypeRepository;
    private readonly IReceiptFieldDefinitionRepository _fieldDefinitionRepository =
        fieldDefinitionRepository;
    private readonly IReceiptTypeConfigurationRepository _configurationRepository =
        configurationRepository;

    public void ValidateParameters(ReceiptTypeConfigurationParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateReceiptTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        await ValidateReceiptTypeExistsAsync(companyId, createDto.ReceiptTypeId, ct);
        await ValidateReceiptTypeConfigurationDoesNotExistAsync(
            companyId,
            createDto.ReceiptTypeId,
            ct
        );

        ValidateDuplicateFieldDefinitions(createDto.FieldConfigurations!);

        foreach (var fieldConfiguration in createDto.FieldConfigurations!)
        {
            var fieldDefinition = await GetFieldDefinitionAsync(
                companyId,
                fieldConfiguration.FieldDefinitionId,
                ct
            );

            ValidateExistsRequiredRule(
                fieldConfiguration.Exists!.Value,
                fieldConfiguration.IsRequired!.Value
            );
            ValidatePlacementRule(fieldDefinition.AllowedPlacement, fieldConfiguration.Placement!.Value);
        }
    }

    private async Task ValidateReceiptTypeExistsAsync(
        Guid companyId,
        Guid receiptTypeId,
        CancellationToken ct
    )
    {
        var exists = await _receiptTypeRepository.AnyAsync(
            e => e.CompanyId == companyId && e.Id == receiptTypeId,
            ct
        );

        if (!exists)
            throw new ReceiptTypeNotFoundException();
    }

    private async Task ValidateReceiptTypeConfigurationDoesNotExistAsync(
        Guid companyId,
        Guid receiptTypeId,
        CancellationToken ct
    )
    {
        var exists = await _configurationRepository.AnyAsync(
            e => e.CompanyId == companyId && e.ReceiptTypeId == receiptTypeId,
            ct
        );

        if (exists)
            throw new ReceiptTypeConfigurationAlreadyExistsException(receiptTypeId);
    }

    private static void ValidateDuplicateFieldDefinitions(
        IReadOnlyCollection<CreateReceiptTypeFieldConfigurationDto> fieldConfigurations
    )
    {
        var duplicate = fieldConfigurations
            .GroupBy(e => e.FieldDefinitionId)
            .FirstOrDefault(e => e.Count() > 1);

        if (duplicate is not null)
            throw new ReceiptTypeConfigurationFieldDefinitionDuplicateException(duplicate.Key);
    }

    private async Task<ReceiptFieldDefinition> GetFieldDefinitionAsync(
        Guid companyId,
        Guid fieldDefinitionId,
        CancellationToken ct
    )
    {
        return await _fieldDefinitionRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == fieldDefinitionId,
            trackChanges: false,
            ct
        ) ?? throw new ReceiptFieldDefinitionNotFoundException();
    }

    private static void ValidateExistsRequiredRule(
        bool exists,
        bool isRequired
    )
    {
        if (!exists && isRequired)
            throw new ReceiptTypeFieldConfigurationRequiredWithoutExistsException();
    }

    private static void ValidatePlacementRule(
        ReceiptFieldPlacement allowedPlacement,
        ReceiptConfiguredPlacement configuredPlacement
    )
    {
        var configuredAsFieldPlacement = configuredPlacement switch
        {
            ReceiptConfiguredPlacement.Header => ReceiptFieldPlacement.Header,
            ReceiptConfiguredPlacement.Detail => ReceiptFieldPlacement.Detail,
            _ => throw new ReceiptTypeFieldConfigurationPlacementNotAllowedException()
        };

        if (!allowedPlacement.HasFlag(configuredAsFieldPlacement))
            throw new ReceiptTypeFieldConfigurationPlacementNotAllowedException();
    }
}
