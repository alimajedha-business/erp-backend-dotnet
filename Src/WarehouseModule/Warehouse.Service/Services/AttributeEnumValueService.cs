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
        var entity = await GetByIdOrThrowAsync(attributeId, id, trackChanges, ct);
        return _mapper.Map<AttributeEnumValueDto>(entity);
    }

    public virtual async Task<AttributeEnumValueDto> PatchAsync(
        Guid attributeId,
        Guid id,
        JsonPatchDocument<PatchAttributeEnumValueDto> patchDocument,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            attributeId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<PatchAttributeEnumValueDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _enumValueRepository.SaveChangesAsync(ct);
        return _mapper.Map<AttributeEnumValueDto>(entity);
    }

    public virtual async Task DeleteAsync(
        Guid attributeId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            attributeId,
            id,
            trackChanges: true,
            ct
        );

        _enumValueRepository.Remove(entity);
        await _enumValueRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(
        Guid attributeId,
        CancellationToken ct
    )
    {
        return _enumValueRepository.GetNextCodeAsync(attributeId, ct);
    }

    private async Task<AttributeEnumValue> GetByIdOrThrowAsync(
        Guid attributeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        // TODO: add specification if needed
        // TODO: check if this attribute enum value belongs to the attribute
        var entity = await _enumValueRepository.GetByIdAsync(
            id,
            trackChanges,
            spec: null,
            ct
        );

        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
