using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;

namespace NGErp.Warehouse.Service.Service.Contracts;

public interface ICategoryBusinessRuleValidator
{
    void ValidateParameters(CategoryParameters parameters);

    void ValidateHasNextLevel(
        int levelNo,
        bool hasNextLevel
    );

    Task ValidateParentAsync(
        Guid companyId,
        Guid? parentCategoryId,
        int levelNo,
        CancellationToken ct
    );

    Task ValidateCategoryCodeLengthAsync(
        Guid companyId,
        int levelNo,
        string code,
        CancellationToken ct
    );

    Task CheckDeletePermissionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );

    Task ValidateCreateAsync(
        Guid companyId,
        CreateCategoryDto createDto,
        CancellationToken ct
    );
}
