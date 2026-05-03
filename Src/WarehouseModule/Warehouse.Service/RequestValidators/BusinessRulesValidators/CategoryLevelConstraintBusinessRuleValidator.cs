using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class CategoryLevelConstraintBusinessRuleValidator(
    ICategoryLevelConstraintRepository constraintRepository,
    ICategoryRepository categoryRepository
) : ICategoryLevelConstraintBusinessRuleValidator
{
    private readonly ICategoryLevelConstraintRepository _constraintRepository =
        constraintRepository;

    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateCategoryLevelConstraintDto createDto,
        CancellationToken ct
    )
    {
        var levelNo = createDto.LevelNo;

        var levelExists = await _constraintRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.LevelNo == levelNo,
            ct
        );

        if (levelExists)
            throw new CategoryLevelConstraintLevelAlreadyExistsException(levelNo);

        if (levelNo == 1)
            return;

        var previousLevelNo = levelNo - 1;
        var previousLevelExists = await _constraintRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.LevelNo == previousLevelNo,
            ct
        );

        if (!previousLevelExists)
        {
            throw new CategoryLevelConstraintPreviousLevelRequiredException(
                previousLevelNo,
                levelNo
            );
        }
    }

    public async Task ValidateCodeLengthChangeAsync(
        Guid companyId,
        int levelNo,
        int currentCodeLength,
        int newCodeLength,
        CancellationToken ct
    )
    {
        if (newCodeLength >= currentCodeLength)
            return;

        var hasCategoriesAtLevel = await _categoryRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.LevelNo == levelNo,
            ct
        );

        if (hasCategoriesAtLevel)
        {
            throw new CategoryLevelConstraintCodeLengthCannotDecreaseException(
                levelNo,
                currentCodeLength,
                newCodeLength
            );
        }
    }
}
