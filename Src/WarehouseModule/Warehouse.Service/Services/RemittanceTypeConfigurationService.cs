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

public class RemittanceTypeConfigurationService(
    IRemittanceTypeConfigurationRepository configurationRepository,
    IRemittanceTypeConfigurationBusinessRuleValidator businessRuleValidator,
    IValidator<CreateRemittanceTypeConfigurationDto> createValidator,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IRemittanceTypeConfigurationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
    private readonly IRemittanceTypeConfigurationRepository _configurationRepository =
        configurationRepository;
    private readonly IRemittanceTypeConfigurationBusinessRuleValidator _businessRuleValidator =
        businessRuleValidator;
    private readonly IValidator<CreateRemittanceTypeConfigurationDto> _createValidator =
        createValidator;

    public async Task CreateAsync(
        Guid companyId,
        CreateRemittanceTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<RemittanceTypeConfiguration>(createDto);
        entity.CompanyId = companyId;

        await _configurationRepository.AddAsync(entity, ct);
        await _configurationRepository.SaveChangesAsync(ct);
    }

    public async Task<RemittanceTypeConfigurationDto?> GetByRemittanceTypeIdAsync(
        Guid companyId,
        Guid remittanceTypeId,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await _configurationRepository.SingleOrDefaultAsync(
            e => e.CompanyId == companyId && e.RemittanceTypeId == remittanceTypeId,
            trackChanges,
            ct
        );

        return configuration != null
            ? Localize(_mapper.Map<RemittanceTypeConfigurationDto>(configuration))
            : null;
    }

    private RemittanceTypeConfigurationDto Localize(RemittanceTypeConfigurationDto dto)
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
