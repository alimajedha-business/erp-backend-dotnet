using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
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
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class RemittanceTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IRemittanceTypeRepository remittanceTypeRepository,
    IRemittanceTypeBusinessRuleValidator businessRuleValidator,
    IValidator<CreateRemittanceTypeDto> createValidator,
    IValidator<PatchRemittanceTypeDto> patchValidator,
    IMapper mapper
) : IRemittanceTypeService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IRemittanceTypeRepository _remittanceTypeRepository = remittanceTypeRepository;
    private readonly IRemittanceTypeBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateRemittanceTypeDto> _createValidator = createValidator;
    private readonly IValidator<PatchRemittanceTypeDto> _patchValidator = patchValidator;

    public async Task<RemittanceTypeDto> CreateAsync(
        Guid companyId,
        CreateRemittanceTypeDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<RemittanceType>(createDto);
        entity.CompanyId = companyId;

        var created = await _remittanceTypeRepository.AddAsync(entity, ct);

        await _remittanceTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<RemittanceTypeDto>(created);
    }

    public async Task<RemittanceTypeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var remittanceType = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        return _mapper.Map<RemittanceTypeDto>(remittanceType);
    }

    public async Task<ListResponseModel<RemittanceTypeSlimDto>> FilterByQAsync(
        Guid companyId,
        RemittanceTypeParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _remittanceTypeRepository.FilterByQ(companyId, parameters);
        var res = await _remittanceTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<RemittanceTypeSlimDto>(
            results: _mapper.Map<IReadOnlyList<RemittanceTypeSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<RemittanceTypeDto>> GetFilteredAsync(
        Guid companyId,
        RemittanceTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<RemittanceType>(filterNodeDto);
        var query = _remittanceTypeRepository.GetFiltered(companyId, advancedFilters);
        var res = await _remittanceTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<RemittanceTypeDto>(
            results: _mapper.Map<IReadOnlyList<RemittanceTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<RemittanceTypeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchRemittanceTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchRemittanceTypePolicy.Validate(patchDocument);

        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchRemittanceTypeDto.Code)
        );

        var remittanceType = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchRemittanceTypeDto>(remittanceType);
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

        if (codePatched && patchDto.Code.HasValue)
        {
            await _businessRuleValidator.ValidateRemittanceTypeCodeUniquenessAsync(
                companyId,
                excludedRemittanceTypeId: id,
                patchDto.Code.Value,
                ct
            );
        }

        _mapper.Map(patchDto, remittanceType);

        await _remittanceTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<RemittanceTypeDto>(remittanceType);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);
        await _remittanceTypeRepository.Remove(
            e =>
                e.CompanyId == companyId &&
                e.Id == id,
            ct
        );
    }

    public Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _remittanceTypeRepository.GetNextCodeAsync(companyId, ct);
    }

    public async Task<RemittanceType> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<RemittanceType, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _remittanceTypeRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new RemittanceTypeNotFoundException();
    }
}
