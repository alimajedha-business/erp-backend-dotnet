using Microsoft.EntityFrameworkCore;

using NGErp.Base.Service.Repository.Contracts;

namespace NGErp.Warehouse.Service.Specifications;

public class WarehouseSpecification :
    ISpecification<Domain.Entities.Warehouse>
{
    public Func<
        IQueryable<Domain.Entities.Warehouse>,
        IQueryable<Domain.Entities.Warehouse>
    > Query => query => query
        .Include(i => i.WarehouseType)
        .Include(i => i.CompanyUnit);
}

public class WarehouseListSpecification :
    ISpecification<Domain.Entities.Warehouse>
{
    public Func<
        IQueryable<Domain.Entities.Warehouse>,
        IQueryable<Domain.Entities.Warehouse>
    > Query => query => query
        .Include(i => i.WarehouseType)
        .Include(i => i.CompanyUnit);
}
