using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptTypeConfigurationService(
    IReceiptTypeConfigurationRepository configurationRepository,
    IReceiptTypeConfigurationBusinessRuleValidator businessRuleValidator,
    IValidator<CreateReceiptTypeConfigurationDto> createValidator,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptTypeConfigurationService
{
    private readonly IReceiptTypeConfigurationRepository _configurationRepository =
        configurationRepository;
    private readonly IReceiptTypeConfigurationBusinessRuleValidator _businessRuleValidator =
        businessRuleValidator;
    private readonly IValidator<CreateReceiptTypeConfigurationDto> _createValidator =
        createValidator;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task CreateAsync(
        Guid companyId,
        CreateReceiptTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<ReceiptTypeConfiguration>(createDto);
        entity.CompanyId = companyId;

        foreach (var fieldConfiguration in entity.FieldConfigurations)
        {
            fieldConfiguration.CompanyId = companyId;
        }

        await _configurationRepository.AddAsync(entity, ct);
        await _configurationRepository.SaveChangesAsync(ct);
    }

    public async Task<ReceiptTypeConfigurationDto?> GetByReceiptTypeIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await _configurationRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.ReceiptTypeId == receiptTypeId,
            trackChanges,
            ct
        );

        return configuration != null
            ? Localize(_mapper.Map<ReceiptTypeConfigurationDto>(configuration))
            : null;
    }

    private ReceiptTypeConfigurationDto Localize(ReceiptTypeConfigurationDto dto)
    {
        return dto with
        {
            FieldConfigurations = dto.FieldConfigurations
                .Select(fieldConfiguration => fieldConfiguration with
                {
                    FieldDefinitionTitle = _localizer[fieldConfiguration.FieldDefinitionTitle].Value
                })
                .ToList()
        };
    }
}
