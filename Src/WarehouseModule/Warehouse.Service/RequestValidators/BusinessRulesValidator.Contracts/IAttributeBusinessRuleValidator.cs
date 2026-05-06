using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

public interface IAttributeBusinessRuleValidator
{
    void ValidateParameters(AttributeParameters parameters);

    Task ValidateCreateAsync(
        Guid companyId,
        CreateAttributeDto createDto,
        CancellationToken ct
    );

    Task ValidateAttributeCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedAttributeId,
        int code,
        CancellationToken ct
    );

    Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task ValidateDataTypeChangeAsync(
        Guid companyId,
        Guid id,
        AttributeDataType currentDataType,
        AttributeDataType newDataType,
        CancellationToken ct
    );
}
