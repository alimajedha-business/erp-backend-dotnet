using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class AttributeService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeRepository attributeRepository,
    IAttributeBusinessRuleValidator businessRuleValidator,
    IValidator<CreateAttributeDto> createValidator,
    IValidator<PatchAttributeDto> patchValidator,
    IMapper mapper
) : IAttributeService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IAttributeRepository _attributeRepository = attributeRepository;
    private readonly IAttributeBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateAttributeDto> _createValidator = createValidator;
    private readonly IValidator<PatchAttributeDto> _patchValidator = patchValidator;

    public async Task<AttributeDto> CreateAsync(
        Guid companyId,
        CreateAttributeDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<Domain.Entities.Attribute>(createDto);
        entity.CompanyId = companyId;

        var created = await _attributeRepository.AddAsync(entity, ct);

        await _attributeRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeDto>(created);
    }

    public async Task<AttributeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var attribute = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        return _mapper.Map<AttributeDto>(attribute);
    }

    public async Task<ListResponseModel<AttributeSlimDto>> FilterByQAsync(
        Guid companyId,
        AttributeParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _attributeRepository.FilterByQ(companyId, parameters);
        var res = await _attributeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<AttributeSlimDto>(
            results: _mapper.Map<IReadOnlyList<AttributeSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<AttributeDto>> GetFilteredAsync(
        Guid companyId,
        AttributeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<Domain.Entities.Attribute>(filterNodeDto);
        var query = _attributeRepository.GetFiltered(companyId, advancedFilters);
        var res = await _attributeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<AttributeDto>(
            results: _mapper.Map<IReadOnlyList<AttributeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<AttributeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchAttributeDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchAttributePolicy.Validate(patchDocument);

        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchAttributeDto.Code)
        );

        var dataTypePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchAttributeDto.DataType)
        );

        var attribute = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchAttributeDto>(attribute);
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
            await _businessRuleValidator.ValidateAttributeCodeUniquenessAsync(
                companyId,
                excludedAttributeId: id,
                patchDto.Code.Value,
                ct
            );
        }

        if (dataTypePatched && patchDto.DataType.HasValue)
        {
            await _businessRuleValidator.ValidateDataTypeChangeAsync(
                companyId,
                id,
                attribute.DataType,
                patchDto.DataType.Value,
                ct
            );
        }

        _mapper.Map(patchDto, attribute);

        await _attributeRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeDto>(attribute);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);
        await _attributeRepository.Remove(e =>
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
        return _attributeRepository.GetNextCodeAsync(companyId, ct);
    }

    public async Task<Domain.Entities.Attribute> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<Domain.Entities.Attribute, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _attributeRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new AttributeNotFoundException();
    }
}


