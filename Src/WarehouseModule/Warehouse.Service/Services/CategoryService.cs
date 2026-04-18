using AutoMapper;

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
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class CategoryService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : ICategoryService
{
    private readonly string _key = "Category";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<Category>(createDto);
        entity.CompanyId = companyId;

        var created = await _categoryRepository.AddAsync(entity, ct);

        await _categoryRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryDto>(created);
    }

    public async Task<CategoryDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<CategoryDto>(entity);
    }

    public async Task<ListResponseModel<CategoryDto>> FilterByQAsync(
        Guid companyId,
        CategoryParameters parameters,
        CancellationToken ct = default
    )
    {
        var query = _categoryRepository.FilterByQ(companyId, parameters);
        var res = await _categoryRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategoryDto>(
            results: _mapper.Map<IReadOnlyList<CategoryDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<CategoryDto>> GetFilteredAsync(
        Guid companyId,
        CategoryParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        var advancedFilters = _filterBuilder.Build<Category>(filterNodeDto);
        var query = _categoryRepository.GetFiltered(companyId, advancedFilters);
        var res = await _categoryRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<CategoryDto>(
            results: _mapper.Map<IReadOnlyList<CategoryDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<CategoryDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchCategoryDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _categoryRepository.SaveChangesAsync(ct);
        return _mapper.Map<CategoryDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        _categoryRepository.Remove(entity);
        await _categoryRepository.SaveChangesAsync(ct);
    }

    private async Task<Category> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _categoryRepository.GetByIdAsync(
            companyId,
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
