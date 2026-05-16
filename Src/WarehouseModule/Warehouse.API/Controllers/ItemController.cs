using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/items")]
public class ItemController(
    IItemService itemService,
    IExcelExportService excelExportService
) : ControllerBase
{
    private readonly IItemService _itemService = itemService;
    private readonly IExcelExportService _excelExportService = excelExportService;

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    [SwaggerRequestExample(typeof(object), typeof(ItemAdvancedSearchExample))]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ListResponseModel<ItemListDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ItemsGetListExample))]

    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] ItemParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _itemService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPost("excel")]
    [InherentlyAction(ActionType.Export)]
    [SwaggerRequestExample(typeof(object), typeof(ItemAdvancedSearchExample))]
    public async Task<IActionResult> ExportToExcel(
        [FromRoute] Guid companyId,
        [FromBody] FilterNodeDto? filterNodeDto,
        [FromQuery] string? columns,
        CancellationToken ct
    )
    {
        var parameters = new ItemParameters
        {
            Paginated = false,
        };

        var result = await _itemService.GetFilteredAsync(
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

        Response.Headers.Append("Content-Disposition", "attachment; filename=\"items.xlsx\"");
        return File(fileBytes.FileContents, fileBytes.ContentType);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _itemService.GetByIdAsync(companyId, id, ct);
        return Ok(dto);
    }
}
