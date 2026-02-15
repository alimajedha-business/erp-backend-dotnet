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

public class UnitOfMeasurementService(
    IAdvancedFilterBuilder filterBuilder,
    IUnitOfMeasurementRepository unitOfMeasurementRepository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : BaseServiceWithCompany<
        UnitOfMeasurement,
        UnitOfMeasurementDto,
        UnitOfMeasurementParameters,
        IUnitOfMeasurementRepository,
        WarehouseResource
    >(
        filterBuilder,
        unitOfMeasurementRepository,
        companyService,
        mapper,
        localizer
    ),
    IUnitOfMeasurementService
{
        protected override string LocalizerKey => "UnitOfMeasurement";
    
        public Task<UnitOfMeasurementDto> CreateUnitOfMeasurementAsync(
            Guid companyId,
            CreateUnitOfMeasurementDto createUnitOfMeasurementDto,
            CancellationToken ct
        )
        {
            return CreateAsync(companyId, createUnitOfMeasurementDto, ct);
        }
    
        public Task<ListResponseModel<UnitOfMeasurementDto>> GetAllUnitOfMeasurementsAsync(
            Guid companyId,
            UnitOfMeasurementParameters unitOfMeasurementParameters,
            CancellationToken ct,
            FilterNodeDto? filterNodeDto = null
        )
        {
            return GetAllAsync(companyId, unitOfMeasurementParameters, ct, filterNodeDto);
        }
    
        public Task<UnitOfMeasurementDto> GetUnitOfMeasurementByIdAsync(
            Guid companyId,
            Guid id,
            CancellationToken ct
        )
        {
            return GetByIdAsync(companyId, id, ct);
        }
    
        public Task<UnitOfMeasurementDto> PatchUnitOfMeasurementAsync(
            Guid companyId,
            Guid id,
            JsonPatchDocument<PatchUnitOfMeasurementDto> patchDoc,
            CancellationToken ct
        )
        {
            return PatchAsync(companyId, id, patchDoc, ct);
        }
    
        public Task<bool> DeleteUnitOfMeasurementAsync(
            Guid companyId,
            Guid id,
            CancellationToken ct
        )
        {
            return DeleteAsync(companyId, id, ct);
    }
}
