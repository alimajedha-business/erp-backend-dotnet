using NGErp.Base.Domain.Exceptions;

namespace NGErp.Warehouse.Domain.Exceptions;

public sealed class RemittanceFieldDefinitionNotFoundException()
    : NotFoundException("RemittanceFieldDefinition");
