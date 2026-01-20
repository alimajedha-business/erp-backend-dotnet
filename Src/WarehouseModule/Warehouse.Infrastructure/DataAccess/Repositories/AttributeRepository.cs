using NGErp.Base.Infrastructure.DataAccess;
using NGErp.Base.Infrastructure.DataAccess.Repositories;
using NGErp.Warehouse.Service.Repository.Contracts;

namespace NGErp.Warehouse.Infrastructure.DataAccess.Repositories;

public class AttributeRepository :
    Repository<Domain.Entities.Attribute>,
    IAttributeRepository
{
    public AttributeRepository(MainDbContext context) : base(context) { }
}
