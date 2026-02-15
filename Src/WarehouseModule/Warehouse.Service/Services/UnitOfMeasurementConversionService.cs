using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Resources;

namespace NGErp.Warehouse.Service.Services;

public class UnitOfMeasurementConversionService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementConversionRepository unitOfMeasurementConversionRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        UnitOfMeasurementConversion,
        UnitOfMeasurementConversionDto,
        UnitOfMeasurementConversionListDto,
        UnitOfMeasurementConversionParameters,
        IUnitOfMeasurementConversionRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementConversionRepository,
        companyService,
        mapper,
        localizer
    ),
    IUnitOfMeasurementConversionService
{
    protected override string LocalizerKey => "UnitOfMeasurementConversion";

    public Task<UnitOfMeasurementConversionDto> CreateUnitOfMeasurementConversionAsync(
        Guid companyId,
        CreateUnitOfMeasurementConversionDto createUnitOfMeasurementConversionDto,
        CancellationToken ct
    )
    {
        return CreateAsync(companyId, createUnitOfMeasurementConversionDto, ct);
    }

    public async Task<ListResponseModel<UnitOfMeasurementConversionListDto>> GetAllUnitOfMeasurementConversionsAsync(
        Guid companyId,
        UnitOfMeasurementConversionParameters parameters,
        CancellationToken ct,
        FilterNodeDto? filterNodeDto = null
    )
    {
        return await GetAllAsync(companyId, parameters, ct, filterNodeDto);
    }

    public Task<UnitOfMeasurementConversionDto> GetUnitOfMeasurementConversionByIdAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        return GetByIdAsync(companyId, id, ct);
    }

    public Task<UnitOfMeasurementConversionDto> PatchUnitOfMeasurementConversionAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchUnitOfMeasurementConversionDto> patchDoc,
        CancellationToken ct
    )
    {
        return PatchAsync(companyId, id, patchDoc, ct);
    }

    public Task DeleteUnitOfMeasurementConversionAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        return DeleteAsync(companyId, id, ct);
    }
}