using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt-type-configurations")]
public class ReceiptTypeConfigurationController(
    IReceiptTypeConfigurationService configurationService
) : ControllerBase
{
    private readonly IReceiptTypeConfigurationService _configurationService =
        configurationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateReceiptTypeConfigurationDto),
        typeof(CreateReceiptTypeConfigurationExample)
    )]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateReceiptTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        await _configurationService.CreateAsync(companyId, createDto, ct);
        return Ok();
    }

    [HttpGet("by-receipt-type/{receiptTypeId:guid}")]
    public async Task<IActionResult> GetByReceiptTypeId(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.GetByReceiptTypeIdAsync(
            companyId,
            receiptTypeId,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }
}
