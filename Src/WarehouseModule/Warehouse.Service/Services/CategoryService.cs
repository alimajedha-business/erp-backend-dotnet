using AutoMapper;

using Microsoft.Extensions.Localization;

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
}
