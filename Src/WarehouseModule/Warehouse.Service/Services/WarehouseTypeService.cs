using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseTypeRepository warehouseTypeRepository,
    IWarehouseTypeBusinessRuleValidator businessRuleValidator,
    IValidator<CreateWarehouseTypeDto> createValidator,
    IValidator<PatchWarehouseTypeDto> patchValidator,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IWarehouseTypeService
{
    private readonly string _key = "WarehouseType";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseTypeRepository _warehouseTypeRepository = warehouseTypeRepository;
    private readonly IWarehouseTypeBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateWarehouseTypeDto> _createValidator = createValidator;
    private readonly IValidator<PatchWarehouseTypeDto> _patchValidator = patchValidator;

    public async Task<WarehouseTypeDto> CreateAsync(
        CreateWarehouseTypeDto createDto,
        CancellationToken ct
    )
    {
        ThrowIfNull(createDto);
        await ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(createDto, ct);

        var entity = _mapper.Map<WarehouseType>(createDto);
        var created = await _warehouseTypeRepository.AddAsync(entity, ct);

        await _warehouseTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseTypeDto>(created);
    }

    public async Task<WarehouseTypeDto> GetByIdAsync(
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var warehouseType = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.Id == id,
            ct
        );

        return _mapper.Map<WarehouseTypeDto>(warehouseType);
    }

    public async Task<ListResponseModel<WarehouseTypeSlimDto>> FilterByQAsync(
        WarehouseTypeParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _warehouseTypeRepository.FilterByQ(parameters);
        var res = await _warehouseTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseTypeSlimDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseTypeSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseTypeDto>> GetFilteredAsync(
        WarehouseTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<WarehouseType>(filterNodeDto);
        var query = _warehouseTypeRepository.GetFiltered(advancedFilters);
        var res = await _warehouseTypeRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseTypeDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<WarehouseTypeDto> PatchAsync(
        Guid id,
        JsonPatchDocument<PatchWarehouseTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchWarehouseTypePolicy.Validate(patchDocument);

        var codePatched = HasProperty(
            patchDocument,
            nameof(PatchWarehouseTypeDto.Code)
        );

        var warehouseType = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchWarehouseTypeDto>(warehouseType);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await ValidateAsync(_patchValidator, patchDto, ct);

        if (codePatched && patchDto.Code.HasValue)
        {
            await _businessRuleValidator.ValidateWarehouseTypeCodeUniquenessAsync(
                excludedWarehouseTypeId: id,
                patchDto.Code.Value,
                ct
            );
        }

        _mapper.Map(patchDto, warehouseType);

        await _warehouseTypeRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseTypeDto>(warehouseType);
    }

    public virtual async Task DeleteAsync(
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(id, ct);
        await _warehouseTypeRepository.Remove(e => e.Id == id, ct);
    }

    public Task<int> GetNextCode(CancellationToken ct)
    {
        return _warehouseTypeRepository.GetNextCodeAsync(ct);
    }

    private async Task<WarehouseType> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<WarehouseType, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _warehouseTypeRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }

    private static bool HasProperty(
        JsonPatchDocument<PatchWarehouseTypeDto> doc,
        string propertyName
    )
    {
        var path = "/" + propertyName.ToLowerInvariant();

        return doc.Operations.Any(op =>
            op.path is not null &&
            op.path.Equals(path, StringComparison.InvariantCultureIgnoreCase)
        );
    }

    private static void ThrowIfNull(CreateWarehouseTypeDto? createDto)
    {
        if (createDto is not null)
            return;

        throw new ValidationException([
            new ValidationFailure()
        ]);
    }

    private static async Task ValidateAsync<T>(
        IValidator<T> validator,
        T dto,
        CancellationToken ct
    )
    {
        var validationResult = await validator.ValidateAsync(dto, ct);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}


