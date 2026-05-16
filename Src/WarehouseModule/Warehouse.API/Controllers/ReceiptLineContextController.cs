using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipts/lines")]
public class ReceiptLineContextController(
    IReceiptLineContextService receiptLineContextService
) : ControllerBase
{
    private readonly IReceiptLineContextService _receiptLineContextService = receiptLineContextService;

    [HttpGet("items/{itemId:guid}/context")]
    public async Task<IActionResult> GetItemContext(
        [FromRoute] Guid companyId,
        [FromRoute] Guid itemId,
        CancellationToken ct
    )
    {
        var dto = await _receiptLineContextService.GetAsync(companyId, itemId, ct);
        return Ok(dto);
    }
}
