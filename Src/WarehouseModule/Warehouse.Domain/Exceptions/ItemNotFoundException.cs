using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public class ItemNotFoundException(Guid id) :
    NotFoundException("ItemNotFound", id)
{
}
