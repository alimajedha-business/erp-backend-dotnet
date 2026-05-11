using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidators;

public class ItemBusinessRuleValidator(
    IItemRepository itemRepository
) : IItemBusinessRuleValidator
{
    private const int MaxItemUnitOfMeasurementCount = 3;

    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "code",
        "title"
    };

    private readonly IItemRepository _itemRepository = itemRepository;

    public void ValidateParameters(ItemParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateItemDto createDto,
        CancellationToken ct
    )
    {
        await ValidateItemCodeUniquenessAsync(
            companyId,
            excludedItemId: null,
            createDto.Code,
            ct
        );

        ValidateItemUnitOfMeasurementCount(createDto.ItemUnitOfMeasurements);
    }

    public async Task ValidateItemCodeUniquenessAsync(
        Guid companyId,
        Guid? excludedItemId,
        string code,
        CancellationToken ct
    )
    {
        var exists = excludedItemId is null
            ? await _itemRepository.AnyAsync(
                e => e.CompanyId == companyId && e.Code == code,
                ct
            )
            : await _itemRepository.AnyAsync(
                e =>
                    e.CompanyId == companyId &&
                    e.Id != excludedItemId.Value &&
                    e.Code == code,
                ct
            );

        if (exists)
            throw new ItemCodeAlreadyExistsException(code);
    }

    public void ValidateItemUnitOfMeasurementCount(
        IEnumerable<CreateItemUnitOfMeasurementDto>? itemUnitOfMeasurements
    )
    {
        var count = itemUnitOfMeasurements?
            .Select(e => e.UnitOfMeasurementId)
            .Distinct()
            .Count() ?? 0;

        if (count > MaxItemUnitOfMeasurementCount)
            throw new ItemUnitOfMeasurementCountExceededException(
                MaxItemUnitOfMeasurementCount
            );
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid categoryId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _itemRepository.AnyAsync(
            e =>
                e.CompanyId == companyId &&
                e.CategoryId == categoryId &&
                e.Id == id,
            ct
        );

        if (!exists)
            throw new ItemNotFoundException();
    }
}
