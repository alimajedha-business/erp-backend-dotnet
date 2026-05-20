using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class RemittanceTypeConfigurationBusinessRuleValidator(
    IRemittanceTypeRepository remittanceTypeRepository,
    IRemittanceFieldDefinitionRepository fieldDefinitionRepository,
    IRemittanceTypeConfigurationRepository configurationRepository
) : IRemittanceTypeConfigurationBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "remittanceTypeId"
    };

    private readonly IRemittanceTypeRepository _remittanceTypeRepository = remittanceTypeRepository;
    private readonly IRemittanceFieldDefinitionRepository _fieldDefinitionRepository =
        fieldDefinitionRepository;
    private readonly IRemittanceTypeConfigurationRepository _configurationRepository =
        configurationRepository;

    public void ValidateParameters(RemittanceTypeConfigurationParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateRemittanceTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        await ValidateRemittanceTypeExistsAsync(companyId, createDto.RemittanceTypeId, ct);
        await ValidateRemittanceTypeConfigurationDoesNotExistAsync(
            companyId,
            createDto.RemittanceTypeId,
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

    private async Task ValidateRemittanceTypeExistsAsync(
        Guid companyId,
        Guid remittanceTypeId,
        CancellationToken ct
    )
    {
        var exists = await _remittanceTypeRepository.AnyAsync(
            e => e.CompanyId == companyId && e.Id == remittanceTypeId,
            ct
        );

        if (!exists)
            throw new RemittanceTypeNotFoundException();
    }

    private async Task ValidateRemittanceTypeConfigurationDoesNotExistAsync(
        Guid companyId,
        Guid remittanceTypeId,
        CancellationToken ct
    )
    {
        var exists = await _configurationRepository.AnyAsync(
            e => e.CompanyId == companyId && e.RemittanceTypeId == remittanceTypeId,
            ct
        );

        if (exists)
            throw new RemittanceTypeConfigurationAlreadyExistsException(remittanceTypeId);
    }

    private static void ValidateDuplicateFieldDefinitions(
        IReadOnlyCollection<CreateRemittanceTypeFieldConfigurationDto> fieldConfigurations
    )
    {
        var duplicate = fieldConfigurations
            .GroupBy(e => e.FieldDefinitionId)
            .FirstOrDefault(e => e.Count() > 1);

        if (duplicate is not null)
            throw new RemittanceTypeConfigurationFieldDefinitionDuplicateException(duplicate.Key);
    }

    private async Task<RemittanceFieldDefinition> GetFieldDefinitionAsync(
        Guid companyId,
        Guid fieldDefinitionId,
        CancellationToken ct
    )
    {
        return await _fieldDefinitionRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.Id == fieldDefinitionId,
            trackChanges: false,
            ct
        ) ?? throw new RemittanceFieldDefinitionNotFoundException();
    }

    private static void ValidateExistsRequiredRule(bool exists, bool isRequired)
    {
        if (!exists && isRequired)
            throw new RemittanceTypeFieldConfigurationRequiredWithoutExistsException();
    }

    private static void ValidatePlacementRule(
        RemittanceFieldPlacement allowedPlacement,
        RemittanceConfiguredPlacement configuredPlacement
    )
    {
        var configuredAsFieldPlacement = configuredPlacement switch
        {
            RemittanceConfiguredPlacement.Header => RemittanceFieldPlacement.Header,
            RemittanceConfiguredPlacement.Detail => RemittanceFieldPlacement.Detail,
            _ => throw new RemittanceTypeFieldConfigurationPlacementNotAllowedException()
        };

        if (!allowedPlacement.HasFlag(configuredAsFieldPlacement))
            throw new RemittanceTypeFieldConfigurationPlacementNotAllowedException();
    }
}
