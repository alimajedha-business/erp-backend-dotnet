using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.General.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/warehouses")]
public class WarehouseController(
    IWarehouseService warehouseService,
    IWarehouseTypeService warehouseTypeService,
    IWarehouseLocationService locationService,
    ICompanyUnitService companyUnitService,
    IExcelExportService excelExportService
) : ControllerBase
{
    private readonly IWarehouseService _warehouseService = warehouseService;
    private readonly IWarehouseTypeService _warehouseTypeService = warehouseTypeService;
    private readonly IWarehouseLocationService _locationService = locationService;
    private readonly ICompanyUnitService _companyUnitService = companyUnitService;
    private readonly IExcelExportService _excelExportService = excelExportService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseCreateRequestExample))]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateWarehouseDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _warehouseService.CreateAsync(
            companyId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] WarehouseParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _warehouseService.GetAllAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseAdvancedSearchRequestExample))]
    [ProducesResponseType(typeof(ListResponseModel<WarehouseListDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK,typeof(WarehouseGetListResponseExample))]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] WarehouseParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _warehouseService.GetAllAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPost("excel")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseAdvancedSearchRequestExample))]
    [ProducesResponseType(typeof(ListResponseModel<WarehouseListDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(WarehouseGetListResponseExample))]
    public async Task<IActionResult> ExportToExcel(
        [FromRoute] Guid companyId,
        [FromBody] FilterNodeDto? filterNodeDto,
        [FromQuery] string? columns,
        CancellationToken ct
    )
    {
        var parameters = new WarehouseParameters
        {
            Paginated = false,
        };

        var result = await _warehouseService.GetAllAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        var columnsList = string.IsNullOrWhiteSpace(columns)
            ? []
            : columns
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

        var excludedColumns = new List<string> { "Id" };

        var fileBytes = _excelExportService.ExportToExcel(
            result.Results,
            columnsList,
            excludedColumns
        );

        Response.Headers.Append("Content-Disposition", "attachment; filename=\"warehouse.xlsx\"");
        return File(fileBytes.FileContents, fileBytes.ContentType);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
       [FromRoute] Guid companyId,
       [FromRoute] Guid id,
       CancellationToken ct
    )
    {
        var warehouse = await _warehouseService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(warehouse);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var code = await _warehouseService.GetNextCode(companyId, ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchWarehouseDto>),
        typeof(WarehousePatchRequestExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchWarehouseDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _warehouseService.PatchAsync(
            companyId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _warehouseService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    #region Warehouse Location

    [HttpPost("{warehouseId:guid}/locations")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateWarehouseLocationDto),
        typeof(WarehouseLocationCreateRequestExample))
    ]
    public async Task<IActionResult> CreateLocation(
        [FromRoute] Guid companyId,
        [FromRoute] Guid warehouseId,
        [FromBody] CreateWarehouseLocationDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _locationService.CreateAsync(
            warehouseId,
            createDto,
            ct
        );

        // FIX: warehouse is null in response (not in db)
        return CreatedAtAction(
            nameof(GetLocationById),
            new { companyId, warehouseId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{warehouseId:guid}/locations/filter-by-q")]
    public async Task<IActionResult> GetLocations(
        [FromRoute] Guid warehouseId,
        [FromQuery] WarehouseLocationParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _locationService.GetFilterByQAsync(
            warehouseId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("{warehouseId:guid}/locations/list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseLocationAdvancedSearchRequestExample))]
    [ProducesResponseType(typeof(ListResponseModel<WarehouseLocationListDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(WarehouseLocationGetListResponseExample))]
    public async Task<IActionResult> GetLocationsAdvancedSearch(
        [FromRoute] Guid warehouseId,
        [FromQuery] WarehouseLocationParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _locationService.GetListAsync(
            warehouseId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{warehouseId:guid}/locations/{id:guid}")]
    public async Task<IActionResult> GetLocationById(
        [FromRoute] Guid warehouseId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _locationService.GetByIdAsync(warehouseId, id, ct);
        return Ok(dto);
    }

    [HttpGet("{warehouseId:guid}/locations/new-code")]
    public async Task<IActionResult> GetLocationNextCode(
        [FromRoute] Guid warehouseId,
        CancellationToken ct
    )
    {
        var code = await _locationService.GetNextCodeAsync(warehouseId, ct);
        return Ok(code);
    }

    [HttpPatch("{warehouseId:guid}/locations/{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchWarehouseLocationDto>),
        typeof(WarehouseLocationPatchRequestExample)
    )]
    public async Task<IActionResult> PatchLocation(
        [FromRoute] Guid warehouseId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchWarehouseLocationDto> patchDocument,
        CancellationToken ct
    )
    {
        // TODO: check if the location with the given warehouse does exist.
        var dto = await _locationService.PatchAsync(warehouseId, id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{warehouseId:guid}/locations/{id:guid}")]
    public async Task<IActionResult> DeleteLocation(
        [FromRoute] Guid warehouseId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _locationService.DeleteAsync(warehouseId, id, ct);
        return NoContent();
    }

    #endregion
}
