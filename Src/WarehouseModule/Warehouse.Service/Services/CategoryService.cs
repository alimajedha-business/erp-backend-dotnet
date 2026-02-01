using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createCategoryDto,
        CancellationToken ct
    )
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        category.CompanyId = companyId;

        var createdCategory = await _categoryRepository.AddAsync(category, ct);
        await _categoryRepository.SaveChangesAsync(ct);

        return  _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task<ListResponseModel<CategoryDto>> GetListAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var listQueryResult = await _categoryRepository.GetListAsync(
            companyId,
            categoryParameters,
            requestAdvancedFilters
        );

        return new ListResponseModel<CategoryDto>(
            items: _mapper.Map<IReadOnlyList<CategoryDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            categoryParameters
        );
    }

    public async Task<CategoryDto> GetByIdAsync(Guid companyId, Guid id)
    {
        return _mapper.Map<CategoryDto>(await GetCategoryByIdAsync(companyId, id));
    }

    public async Task<CategoryDto> UpdateAsync(
        Guid companyId,
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    )
    {
        var category = await GetCategoryByIdAsync(companyId, id);

        _mapper.Map(updateCategoryDto, category);
        await _categoryRepository.SaveChangesAsync(ct);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> DeleteAsync(Guid companyId, Guid id)
    {
        _categoryRepository.Remove(await GetCategoryByIdAsync(companyId, id));
        return true;
    }

    private async Task<Category> GetCategoryByIdAsync(Guid companyId, Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(companyId, id);
        return category ?? throw new NotFoundException(_localizer["Category"].Value);
    }
}
