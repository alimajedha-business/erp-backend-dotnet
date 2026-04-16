using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/warehouse/warehouse-types")]
public class WarehouseTypeController(
    IWarehouseTypeService warehouseTypeService,
    IExcelExportService excelExportService
) : ControllerBase
{
    private readonly IWarehouseTypeService _warehouseTypeService = warehouseTypeService;
    private readonly IExcelExportService _excelExportService = excelExportService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(typeof(CreateWarehouseTypeDto), typeof(WarehouseTypeCategoryExample))]
    public async Task<IActionResult> Create(
        [FromBody] CreateWarehouseTypeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _warehouseTypeService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] WarehouseTypeParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _warehouseTypeService.GetAllAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseTypeAdvancedSearchExample))]
    public async Task<IActionResult> GetAdvancedSearch(
        [FromQuery] WarehouseTypeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _warehouseTypeService.GetAllAsync(
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPost("excel")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(WarehouseTypeAdvancedSearchExample))]
    public async Task<IActionResult> ExportToExcel(
        [FromBody] FilterNodeDto? filterNodeDto,
        [FromQuery] string? columns,
        CancellationToken ct
    )
    {
        var parameters = new WarehouseTypeParameters
        {
            Paginated = false,
        };

        var result = await _warehouseTypeService.GetAllAsync(
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

        Response.Headers.Append("Content-Disposition", "attachment; filename=\"warehouse_types.xlsx\"");
        return File(fileBytes.FileContents, fileBytes.ContentType);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _warehouseTypeService.GetByIdAsync(id, trackChanges: false, ct);
        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        CancellationToken ct
    )
    {
        var code = await _warehouseTypeService.GetNextCode(ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchWarehouseTypeDto>),
        typeof(WarehouseTypePatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchWarehouseTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _warehouseTypeService.PatchAsync(
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _warehouseTypeService.DeleteAsync(id, ct);
        return NoContent();
    }
}
