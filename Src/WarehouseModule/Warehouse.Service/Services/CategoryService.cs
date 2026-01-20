using AutoMapper;

using Microsoft.Extensions.Logging;

using NGErp.Warehouse.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Services;

internal class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ILogger<CategoryService> logger,
        IMapper mapper
    )
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto category)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    { 
        throw new NotImplementedException(); 
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid id, UpdateCategoryDto category)
    {
        throw new NotImplementedException(); 
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
