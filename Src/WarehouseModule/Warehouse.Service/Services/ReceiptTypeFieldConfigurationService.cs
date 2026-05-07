using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptTypeFieldConfigurationService(
    IReceiptTypeFieldConfigurationRepository configurationRepository,
    IReceiptTypeConfigurationRepository receiptTypeConfigurationRepository,
    IReceiptTypeFieldConfigurationBusinessRuleValidator businessRuleValidator,
    IValidator<CreateReceiptTypeFieldConfigurationDto> createValidator,
    IValidator<PatchReceiptTypeFieldConfigurationDto> patchValidator,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptTypeFieldConfigurationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
    private readonly IReceiptTypeFieldConfigurationRepository _configurationRepository =
        configurationRepository;
    private readonly IReceiptTypeConfigurationRepository _receiptTypeConfigurationRepository =
        receiptTypeConfigurationRepository;
    private readonly IReceiptTypeFieldConfigurationBusinessRuleValidator _businessRuleValidator =
        businessRuleValidator;
    private readonly IValidator<CreateReceiptTypeFieldConfigurationDto> _createValidator =
        createValidator;
    private readonly IValidator<PatchReceiptTypeFieldConfigurationDto> _patchValidator =
        patchValidator;

    public async Task<ReceiptTypeFieldConfigurationDto> CreateAsync(
        Guid companyId,
        Guid receiptTypeId,
        CreateReceiptTypeFieldConfigurationDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);

        var receiptTypeConfiguration = await GetReceiptTypeConfigurationOrThrowAsync(
            companyId,
            receiptTypeId,
            ct
        );

        await _businessRuleValidator.ValidateCreateAsync(
            companyId,
            receiptTypeConfiguration.Id,
            createDto,
            ct
        );

        var entity = _mapper.Map<ReceiptTypeFieldConfiguration>(createDto);
        entity.CompanyId = companyId;
        entity.ReceiptTypeConfigurationId = receiptTypeConfiguration.Id;

        var created = await _configurationRepository.AddAsync(entity, ct);

        await _configurationRepository.SaveChangesAsync(ct);
        return await GetByIdAsync(companyId, receiptTypeId, created.Id, trackChanges: false, ct);
    }

    public async Task<ReceiptTypeFieldConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            companyId,
            receiptTypeId,
            id,
            trackChanges,
            ct
        );

        return Localize(_mapper.Map<ReceiptTypeFieldConfigurationDto>(configuration));
    }

    public async Task<ReceiptTypeFieldConfigurationDto> PatchAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchReceiptTypeFieldConfigurationPolicy.Validate(patchDocument);

        var configuration = await GetSingleOrThrowAsync(
            companyId,
            receiptTypeId,
            id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchReceiptTypeFieldConfigurationDto>(configuration);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);
        await _businessRuleValidator.ValidatePatchAsync(companyId, configuration, patchDto, ct);

        _mapper.Map(patchDto, configuration);

        await _configurationRepository.SaveChangesAsync(ct);
        return Localize(_mapper.Map<ReceiptTypeFieldConfigurationDto>(configuration));
    }

    public async Task DeleteAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        CancellationToken ct
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            companyId,
            receiptTypeId,
            id,
            trackChanges: true,
            ct
        );

        _configurationRepository.Remove(configuration);
        await _configurationRepository.SaveChangesAsync(ct);
    }

    private async Task<ReceiptTypeFieldConfiguration> GetSingleOrThrowAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        bool trackChanges,
        CancellationToken ct = default
    )
    {
        var entity = await _configurationRepository.SingleOrDefaultAsync(
            e =>
                e.CompanyId == companyId &&
                e.ReceiptTypeConfiguration.ReceiptTypeId == receiptTypeId &&
                e.Id == id,
            trackChanges,
            ct
        );

        return entity ?? throw new ReceiptTypeFieldConfigurationNotFoundException();
    }

    private async Task<ReceiptTypeConfiguration> GetReceiptTypeConfigurationOrThrowAsync(
        Guid companyId,
        Guid receiptTypeId,
        CancellationToken ct = default
    )
    {
        var entity = await _receiptTypeConfigurationRepository.SingleOrDefaultAsync(
            e =>
                e.CompanyId == companyId &&
                e.ReceiptTypeId == receiptTypeId,
            trackChanges: false,
            ct
        );

        return entity ?? throw new ReceiptTypeConfigurationNotFoundException();
    }

    private ReceiptTypeFieldConfigurationDto Localize(ReceiptTypeFieldConfigurationDto dto)
    {
        return dto with
        {
            FieldDefinition = dto.FieldDefinition with
            {
                Title = _localizer[dto.FieldDefinition.Title].Value
            }
        };
    }
}
