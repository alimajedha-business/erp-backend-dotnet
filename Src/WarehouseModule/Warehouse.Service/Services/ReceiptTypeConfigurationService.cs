using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptTypeConfigurationService(
    IAdvancedFilterBuilder filterBuilder,
    IReceiptTypeConfigurationRepository configurationRepository,
    IReceiptTypeConfigurationBusinessRuleValidator businessRuleValidator,
    IValidator<CreateReceiptTypeConfigurationDto> createValidator,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptTypeConfigurationService
{
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IReceiptTypeConfigurationRepository _configurationRepository =
        configurationRepository;
    private readonly IReceiptTypeConfigurationBusinessRuleValidator _businessRuleValidator =
        businessRuleValidator;
    private readonly IValidator<CreateReceiptTypeConfigurationDto> _createValidator =
        createValidator;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task<ReceiptTypeConfigurationDto> CreateAsync(
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

        var created = await _configurationRepository.AddAsync(entity, ct);

        await _configurationRepository.SaveChangesAsync(ct);
        return await GetByIdAsync(companyId, created.Id, trackChanges: false, ct);
    }

    public async Task<ReceiptTypeConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            trackChanges,
            e => e.CompanyId == companyId && e.Id == id,
            ct
        );

        return Localize(_mapper.Map<ReceiptTypeConfigurationDto>(configuration));
    }

    public async Task<ReceiptTypeConfigurationDto> GetByReceiptTypeIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            trackChanges,
            e => e.CompanyId == companyId && e.ReceiptTypeId == receiptTypeId,
            ct
        );

        return Localize(_mapper.Map<ReceiptTypeConfigurationDto>(configuration));
    }

    public async Task<ListResponseModel<ReceiptTypeConfigurationListDto>> FilterByQAsync(
        Guid companyId,
        ReceiptTypeConfigurationParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _configurationRepository.FilterByQ(companyId, parameters);
        var res = await _configurationRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptTypeConfigurationListDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptTypeConfigurationListDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ReceiptTypeConfigurationDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptTypeConfigurationParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<ReceiptTypeConfiguration>(filterNodeDto);
        var query = _configurationRepository.GetFiltered(companyId, advancedFilters);
        var res = await _configurationRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptTypeConfigurationDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptTypeConfigurationDto>>(res.items)
                .Select(Localize)
                .ToList(),
            totalCount: res.count,
            parameters
        );
    }

    public async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            trackChanges: true,
            e => e.CompanyId == companyId && e.Id == id,
            ct
        );

        _configurationRepository.Remove(configuration);
        await _configurationRepository.SaveChangesAsync(ct);
    }

    public async Task<ReceiptTypeConfiguration> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<ReceiptTypeConfiguration, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _configurationRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new ReceiptTypeConfigurationNotFoundException();
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
