using System.Linq.Expressions;

using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class AttributeEnumValueService(
    IAttributeEnumValueRepository enumValueRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IAttributeEnumValueService
{
    private readonly string _key = "AttributeEnumValue";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAttributeEnumValueRepository _enumValueRepository = enumValueRepository;

    public async Task<AttributeEnumValueDto> CreateAsync(
        Guid attributeId,
        CreateAttributeEnumValueDto createDto,
        CancellationToken ct
    )
    {
        var entity = _mapper.Map<AttributeEnumValue>(createDto);
        entity.AttributeId = attributeId;

        var created = await _enumValueRepository.AddAsync(entity, ct);

        await _enumValueRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeEnumValueDto>(created);
    }

    public async Task<AttributeEnumValueDto> GetByIdAsync(
        Guid attributeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var enumValue = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.AttributeId == attributeId && p.Id == id,
            ct
        );

        return _mapper.Map<AttributeEnumValueDto>(enumValue);
    }

    public virtual async Task<AttributeEnumValueDto> PatchAsync(
        Guid attributeId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    )
    {
        var enumValue = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.AttributeId == attributeId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchAttributeEnumValueDto>(enumValue);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, enumValue);

        await _enumValueRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeEnumValueDto>(enumValue);
    }

    public virtual async Task DeleteAsync(
        Guid attributeId,
        Guid id,
        CancellationToken ct
    )
    {
        await _enumValueRepository.Remove(e =>
            e.AttributeId == attributeId &&
            e.Id == id,
            ct
        );
    }

    public Task<int> GetNextCode(
        Guid attributeId,
        CancellationToken ct
    )
    {
        return _enumValueRepository.GetNextCodeAsync(attributeId, ct);
    }

    private async Task<AttributeEnumValue> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<AttributeEnumValue, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _enumValueRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
