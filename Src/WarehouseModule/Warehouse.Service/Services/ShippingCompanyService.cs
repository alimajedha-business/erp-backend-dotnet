using AutoMapper;

using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class ShippingCompanyService(
    IAdvancedFilterBuilder filterBuilder,
    IShippingCompanyRepository shippingCompanyRepository,
    IMapper mapper,
    IValidator<ShippingCompany> validator,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        ShippingCompany,
        ShippingCompanyDto,
        ShippingCompanyListDto,
        ShippingCompanyParameters,
        IShippingCompanyRepository,
        WarehouseResource
    >(
        filterBuilder,
        shippingCompanyRepository,
        mapper,
        validator,
        localizer
    ),
    IShippingCompanyService
{
    protected override string LocalizerKey => "ShippingCompany";
}
