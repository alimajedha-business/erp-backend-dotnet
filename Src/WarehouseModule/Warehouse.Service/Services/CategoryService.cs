using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class CategoryService(
    IAdvancedFilterBuilder filterBuilder,
    ICategoryRepository categoryRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        Category,
        CategoryDto,
        CategoryParameters,
        ICategoryRepository,
        WarehouseResource
    >(
        filterBuilder,
        categoryRepository,
        companyService,
        mapper,
        localizer
    ),
    ICategoryService
{
    protected override string LocalizerKey => "Category";

    public Task<CategoryDto> CreateCategoryAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    ) => CreateAsync(companyId, createDto, ct);

    public Task<ListResponseModel<CategoryDto>> GetAllCategoriesAsync(
        Guid companyId,
        CategoryParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    ) => GetAllAsync(companyId, parameters, ct, filterNodeDto);

    public Task<CategoryDto> GetCategoryByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => GetByIdAsync(companyId, id, ct);

    public Task<CategoryDto> PatchCategoryAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    ) => PatchAsync(companyId, id, patchDocument, ct);

    public Task DeleteCategoryAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    ) => DeleteAsync(companyId, id, ct);
}
