using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
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
        CreateCategoryDto createCategoryDto,
        CancellationToken ct
    )
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        var createdCategory = await _categoryRepository.AddAsync(category, ct);
        await _categoryRepository.SaveChangesAsync(ct);
        return  _mapper.Map<CategoryDto>(createdCategory);
    }

    public async Task<IEnumerable<CategoryDto>> GetPaginatedAsync(
        CategoryParameters categoryParameters,
        string? search,
        object[]? searchParameters
    )
    {
        var categories = await _categoryRepository.GetPaginatedAsync(
            categoryParameters,
            search,
            searchParameters
        );

        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(Guid id)
    {
        return _mapper.Map<CategoryDto>(await GetCategoryByIdAsync(id));
    }

    public async Task<CategoryDto> UpdateAsync(
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    )
    {
        var category = await GetCategoryByIdAsync(id);

        _mapper.Map(updateCategoryDto, category);
        await _categoryRepository.SaveChangesAsync(ct);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _categoryRepository.Remove(await GetCategoryByIdAsync(id));
        return true;
    }

    private async Task<Category> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category ?? throw new NotFoundException(_localizer["Category"].Value);
    }
}
