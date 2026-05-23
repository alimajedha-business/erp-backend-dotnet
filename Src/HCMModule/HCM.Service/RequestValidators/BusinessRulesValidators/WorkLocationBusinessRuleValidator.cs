using NGErp.Base.Service.Validators;
using NGErp.HCM.Domain.Exceptions;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.Repository.Contracts;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.RequestValidators.BusinessRulesValidator.Contracts;

namespace NGErp.HCM.Service.RequestValidators.BusinessRulesValidators;

public class WorkLocationBusinessRuleValidator(
    IWorkLocationRepository workLocationRepository
) : IWorkLocationBusinessRuleValidator
{
    private static readonly HashSet<string> _allowedOrderFields = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        "parentId",
        "title"
    };

    private readonly IWorkLocationRepository _workLocationRepository = workLocationRepository;

    public void ValidateParameters(WorkLocationParameters parameters)
    {
        RequestParametersValidator.ValidateOrdering(parameters, _allowedOrderFields);
    }

    public async Task ValidateCreateAsync(
        Guid companyId,
        CreateWorkLocationDto createDto,
        CancellationToken ct
    )
    {
        await ValidateTitleUniquenessAsync(
            companyId,
            excludedId: null,
            createDto.Title,
            ct
        );

        if (createDto.ParentId.HasValue)
        {
            await ValidateParentExistsAsync(companyId, createDto.ParentId.Value, ct);
        }
    }

    public async Task ValidatePatchAsync(
        Guid companyId,
        Guid id,
        PatchWorkLocationDto patchDto,
        CancellationToken ct
    )
    {
        if (patchDto.Title != null)
        {
            await ValidateTitleUniquenessAsync(
                companyId,
                excludedId: id,
                patchDto.Title,
                ct
            );
        }

        if (patchDto.ParentId.HasValue)
        {
            var newParentId = patchDto.ParentId.Value;
            if (newParentId == id)
                throw new WorkLocationCircularDependencyException();

            await ValidateParentExistsAsync(companyId, newParentId, ct);
            await ValidateCircularDependencyAsync(companyId, id, newParentId, ct);
        }
    }

    public async Task ValidateTitleUniquenessAsync(
        Guid companyId,
        Guid? excludedId,
        string title,
        CancellationToken ct
    )
    {
        var exists = excludedId is null
            ? await _workLocationRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Title == title,
                ct
            )
            : await _workLocationRepository.AnyAsync(e =>
                e.CompanyId == companyId &&
                e.Id != excludedId.Value &&
                e.Title == title,
                ct
            );

        if (exists)
            throw new WorkLocationTitleAlreadyExistsException(title);
    }

    public async Task ValidateDeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var exists = await _workLocationRepository.AnyAsync(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );

        if (!exists)
            throw new WorkLocationNotFoundException();
    }

    private async Task ValidateParentExistsAsync(Guid companyId, Guid parentId, CancellationToken ct)
    {
        var exists = await _workLocationRepository.AnyAsync(
            e => e.CompanyId == companyId && e.Id == parentId,
            ct
        );

        if (!exists)
            throw new WorkLocationNotFoundException();
    }

    private async Task ValidateCircularDependencyAsync(
        Guid companyId,
        Guid id,
        Guid newParentId,
        CancellationToken ct
    )
    {
        var currentParentId = (Guid?)newParentId;
        while (currentParentId != null)
        {
            if (currentParentId == id)
                throw new WorkLocationCircularDependencyException();

            var parent = await _workLocationRepository.SingleOrDefaultAsync(
                p => p.CompanyId == companyId && p.Id == currentParentId.Value,
                trackChanges: false,
                ct
            );

            if (parent == null) break;

            currentParentId = parent.ParentId;
        }
    }
}
