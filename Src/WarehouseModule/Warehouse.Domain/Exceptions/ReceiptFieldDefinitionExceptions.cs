using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class ReceiptFieldDefinitionNotFoundException()
    : NotFoundException("ReceiptFieldDefinition");
