using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Warehouse.Domain.Constants;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt-types/{receiptTypeId:guid}/field-configurations")]
[HasPermission(EntityTypes.ReceiptType)]
public class ReceiptTypeFieldConfigurationController(
    IReceiptTypeFieldConfigurationService configurationService
) : ControllerBase
{
    private readonly IReceiptTypeFieldConfigurationService _configurationService =
        configurationService;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.GetByIdAsync(
            companyId,
            receiptTypeId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }
}