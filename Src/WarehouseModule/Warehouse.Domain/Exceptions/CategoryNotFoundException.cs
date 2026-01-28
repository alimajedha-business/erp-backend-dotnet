using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public class CategoryNotFoundException(Guid id) : 
    NotFoundException("CategoryNotFound", id)
{
}
