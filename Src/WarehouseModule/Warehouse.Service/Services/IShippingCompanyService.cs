using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public interface IShippingCompanyService : IBaseService<
    ShippingCompany,
    ShippingCompanyDto,
    ShippingCompanyListDto,
    ShippingCompanyParameters,
    IShippingCompanyRepository,
    WarehouseResource
>
{ }
