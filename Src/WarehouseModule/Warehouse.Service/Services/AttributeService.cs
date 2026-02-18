using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class AttributeService(
    IAdvancedFilterBuilder filterBuilder,
    IAttributeRepository attributeRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        Domain.Entities.Attribute,
        AttributeDto,
        AttributeParameters,
        IAttributeRepository,
        WarehouseResource
    >(
        filterBuilder,
        attributeRepository,
        companyService,
        mapper,
        localizer
    ),
    IAttributeService
{
    protected override string LocalizerKey => "Attribute";
}
