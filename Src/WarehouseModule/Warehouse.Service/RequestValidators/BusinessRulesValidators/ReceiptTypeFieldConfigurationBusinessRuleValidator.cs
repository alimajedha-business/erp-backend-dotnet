using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ReceiptTypeFieldConfigurationBusinessRuleValidator(
    IReceiptTypeRepository receiptTypeRepository,
    IReceiptFieldDefinitionRepository fieldDefinitionRepository
) : IReceiptTypeFieldConfigurationBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "fieldDefinitionId",
        "exists",
        "isRequired"
    };

    private readonly IReceiptTypeRepository _receiptTypeRepository = receiptTypeRepository;
    private readonly IReceiptFieldDefinitionRepository _fieldDefinitionRepository =
        fieldDefinitionRepository;

    public void ValidateParameters(ReceiptTypeFieldConfigurationParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        Guid receiptTypeId,
        CreateReceiptTypeFieldConfigurationDto createDto,
        CancellationToken ct
    )
    {
        await ValidateReceiptTypeExistsAsync(companyId, receiptTypeId, ct);

        var fieldDefinition = await GetFieldDefinitionAsync(
            companyId,
            createDto.FieldDefinitionId,
            ct
        );

        ValidateExistsRequiredRule(createDto.Exists!.Value, createDto.IsRequired!.Value);
        ValidatePlacementRule(fieldDefinition.AllowedPlacement, createDto.Placement!.Value);
    }

    public async Task ValidatePatchAsync(
        Guid companyId,
        ReceiptTypeFieldConfiguration configuration,
        PatchReceiptTypeFieldConfigurationDto patchDto,
        CancellationToken ct
    )
    {
        var fieldDefinition = configuration.FieldDefinition;
        if (fieldDefinition.CompanyId != companyId)
        {
            fieldDefinition = await GetFieldDefinitionAsync(
                companyId,
                configuration.FieldDefinitionId,
                ct
            );
        }

        ValidateExistsRequiredRule(patchDto.Exists!.Value, patchDto.IsRequired!.Value);
        ValidatePlacementRule(fieldDefinition.AllowedPlacement, patchDto.Placement!.Value);
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
