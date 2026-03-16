using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
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
    ICategoryLevelConstraintService constraintService,
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

    private readonly ICategoryLevelConstraintService _constraintService = constraintService;

    public async Task<CategoryDto> CreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        var categoryLevel = await _constraintService.GetByLevelNo(
            companyId,
            createDto.LevelNo,
            ct
        );

        var maxCodeLength = categoryLevel?.CodeLength ?? 0;
        if (maxCodeLength > 0 && createDto.Code.Length > maxCodeLength)
        {
            // TODO: add proper exception
            throw new NotFoundException();
        }

        return await base.CreateAsync(companyId, createDto, ct);
    }
}
