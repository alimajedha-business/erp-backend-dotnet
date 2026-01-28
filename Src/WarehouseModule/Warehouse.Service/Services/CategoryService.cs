using AutoMapper;

using Microsoft.Extensions.Logging;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    ILogger<CategoryService> logger,
    IMapper mapper
) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ILogger<CategoryService> _logger = logger;
    private readonly IMapper _mapper = mapper;

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto, CancellationToken ct)
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

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category is not null ? _mapper.Map<CategoryDto>(category) : null;
    }

    public async Task<CategoryDto?> UpdateAsync(
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    )
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return null;

        _mapper.Map(updateCategoryDto, category);
        await _categoryRepository.SaveChangesAsync(ct);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
            return false;

        _categoryRepository.Remove(category);
        return true;
    }
}
