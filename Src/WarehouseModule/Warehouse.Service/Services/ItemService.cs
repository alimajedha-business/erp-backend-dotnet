using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NGErp.Warehouse.Service.Services;

public class ItemService(
    IAdvancedFilterBuilder filterBuilder,
    ICompanyService companyService,
    IItemRepository itemRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IItemService
{
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly ICompanyService _companyService = companyService;
    private readonly IItemRepository _itemRepository = itemRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;

    public async Task<ItemDto> CreateItemAsync(
        Guid companyId,
        CreateItemDto createItemDto,
        CancellationToken ct
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var item = _mapper.Map<Item>(createItemDto);
        item.CompanyId = companyId;

        var createdItem = await _itemRepository.AddAsync(item, ct);
        await _itemRepository.SaveChangesAsync(ct);

        return _mapper.Map<ItemDto>(createdItem);
    }

    public async Task<ListResponseModel<ItemDto>> GetAllItemsAsync(
        Guid companyId,
        ItemParameters itemParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var listQueryResult = await _itemRepository.GetAllAsync(
            companyId,
            itemParameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<ItemDto>(
            items: _mapper.Map<IReadOnlyList<ItemDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            itemParameters
        );
    }

    public async Task<ListResponseModel<ItemDto>> GetCategoryAllItemsAsync(
        Guid companyId,
        Guid categoryId,
        ItemParameters itemParameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var advancedFilters = _filterBuilder.Build<Item>(filterNodeDto);
        var listQueryResult = await _itemRepository.GetCategoryAllAsync(
            companyId,
            categoryId,
            itemParameters,
            ct,
            advancedFilters
        );

        return new ListResponseModel<ItemDto>(
            items: _mapper.Map<IReadOnlyList<ItemDto>>(listQueryResult.items),
            totalCount: listQueryResult.count,
            itemParameters
        );
    }

    public async Task<ItemDto?> GetItemByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var item = await GetByIdOrThrowExceptionAsync(companyId,id,ct);
        return _mapper.Map<ItemDto>(item);
    }

    public async Task<ItemDto> PatchItemAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchItemDto> patchItemDto,
        CancellationToken ct
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var item = await GetByIdOrThrowExceptionAsync(
            companyId,
            id,
            ct,
            trackChanges: true
        );

        var patchDto = _mapper.Map<PatchItemDto>(item);
        var errors = new List<string>();

        patchItemDto.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        _mapper.Map(patchItemDto, item);

        try
        {
            await _itemRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateBadRequestException(ex.Message);
        }

        return _mapper.Map<ItemDto>(item);
    }

    public async Task<bool> DeleteItemAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var item = await GetByIdOrThrowExceptionAsync(companyId, id, ct);
        _itemRepository.Remove(item);

        try
        {
            await _itemRepository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new ForeignKeyViolationException(_localizer["Item"].Value);
        }

        return true;
    }

    private async Task<Item> GetByIdOrThrowExceptionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct,
        bool trackChanges = false
    )
    {
        await _companyService.GetCompanyByIdAsync(
            companyId,
            ct
        );

        var item = await _itemRepository.GetByIdAsync(
            companyId,
            id,
            ct,
            trackChanges
        );

        return item ?? throw new NotFoundException(_localizer["Item"].Value);
    }
}
