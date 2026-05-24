using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class FeatureSettingsService(
    IFeatureSettingsRepository featureSettingsRepository,
    IValidator<CreateFeatureSettingsDto> createValidator,
    IValidator<PatchFeatureSettingsDto> patchValidator,
    IMapper mapper
) : IFeatureSettingsService
{
    private readonly IMapper _mapper = mapper;
    private readonly IFeatureSettingsRepository _featureSettingsRepository = featureSettingsRepository;
    private readonly IValidator<CreateFeatureSettingsDto> _createValidator = createValidator;
    private readonly IValidator<PatchFeatureSettingsDto> _patchValidator = patchValidator;

    public async Task<FeatureSettingsDto> CreateAsync(
        Guid companyId,
        CreateFeatureSettingsDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await ValidateCompanySettingsDoNotExistAsync(companyId, ct);

        var entity = _mapper.Map<FeatureSettings>(createDto);
        entity.CompanyId = companyId;

        var created = await _featureSettingsRepository.AddAsync(entity, ct);

        await _featureSettingsRepository.SaveChangesAsync(ct);
        return _mapper.Map<FeatureSettingsDto>(created);
    }

    public async Task<FeatureSettingsDto> GetByIdAsync(
        Guid companyId,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CompanyId == companyId,
            ct
        );

        return _mapper.Map<FeatureSettingsDto>(entity);
    }

    public virtual async Task<FeatureSettingsDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchFeatureSettingsDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchFeatureSettingsPolicy.Validate(patchDocument);

        var entity = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchFeatureSettingsDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);

        _mapper.Map(patchDto, entity);

        await _featureSettingsRepository.SaveChangesAsync(ct);
        return _mapper.Map<FeatureSettingsDto>(entity);
    }

    private async Task<FeatureSettings> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<FeatureSettings, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _featureSettingsRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new FeatureSettingsNotFoundException();
    }

    private async Task ValidateCompanySettingsDoNotExistAsync(
        Guid companyId,
        CancellationToken ct
    )
    {
        var exists = await _featureSettingsRepository.AnyAsync(
            e => e.CompanyId == companyId,
            ct
        );

        if (exists)
            throw new FeatureSettingsAlreadyExistsException();
    }
}
