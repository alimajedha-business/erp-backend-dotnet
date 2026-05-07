using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt-field-definitions")]
public class ReceiptFieldDefinitionController(
    IReceiptFieldDefinitionService fieldDefinitionService
) : ControllerBase
{
    private readonly IReceiptFieldDefinitionService _fieldDefinitionService =
        fieldDefinitionService;

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> GetByQ(
        [FromRoute] Guid companyId,
        [FromQuery] ReceiptFieldDefinitionParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _fieldDefinitionService.FilterByQAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(ReceiptFieldDefinitionAdvancedSearchExample))]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] ReceiptFieldDefinitionParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _fieldDefinitionService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _fieldDefinitionService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }
}
