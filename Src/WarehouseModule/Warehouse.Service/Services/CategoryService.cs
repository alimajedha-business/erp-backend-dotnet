using AutoMapper;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

    public async Task<CategoryDto> CreateCategoryAsync(
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

    public async Task<ListResponseModel<CategoryDto>> GetAllCategoriesAsync(
        Guid companyId,
        CategoryParameters categoryParameters,
        CancellationToken ct,
        RequestAdvancedFilters? requestAdvancedFilters = null
    )
    {
        var listQueryResult = await _categoryRepository.GetAllAsync(
            companyId,
            categoryParameters,
            ct,
            requestAdvancedFilters
        );

        return new ListResponseModel<CategoryDto>(
            items: _mapper.Map<IReadOnlyList<CategoryDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            categoryParameters
        );
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var category = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(
        Guid companyId,
        Guid id,
        UpdateCategoryDto updateCategoryDto,
        CancellationToken ct
    )
    {
        var category = await GetByIdOrThrowExceptionAsync(
            companyId,
            id,
            ct,
            trackChanges: true
        );

        _mapper.Map(updateCategoryDto, category);
        await _categoryRepository.SaveChangesAsync(ct);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<bool> DeleteCategoryAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var category = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _categoryRepository.Remove(category);

        try
        {
            await _categoryRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex) 
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["Category"].Value);
        }

        return true;
    }

    private async Task<Category> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        var category = await _categoryRepository.GetByIdAsync(
            companyId,
            id,
            ct,
            trackChanges
        );

        return category ?? throw new NotFoundException(_localizer["Category"].Value);
    }
}
