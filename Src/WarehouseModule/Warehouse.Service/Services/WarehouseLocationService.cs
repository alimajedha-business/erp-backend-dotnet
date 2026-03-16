using System.Reflection;

using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseLocationService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseLocationRepository locationRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseService<
        WarehouseLocation,
        WarehouseLocationDto,
        WarehouseLocationParameters,
        IWarehouseLocationRepository,
        WarehouseResource
    >(
        filterBuilder,
        locationRepository,
        mapper,
        localizer
    ),
    IWarehouseLocationService
{
    protected override string LocalizerKey => "WarehouseLocation";

    public async Task<WarehouseLocationDto> CreateAsync(
        CreateWarehouseLocationDto createDto,
        CancellationToken ct
    )
    {
        var parentLocationId = createDto.ParentLocationId;
        if (parentLocationId is not null)
        {
            var parentLocation = await _repo.GetByIdAsync(parentLocationId.Value, ct);
            if (parentLocation is null)
            {
                throw new NotFoundException(_localizer[LocalizerKey].Value);
            }

            if (parentLocation.WarehouseId != createDto.WarehouseId)
            {
                throw new NotFoundException(_localizer["WarehouseLocation-Warehouse"].Value);
            }
        }

        return await base.CreateAsync(createDto, ct);
    }
}
