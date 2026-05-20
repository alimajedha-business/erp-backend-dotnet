using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/remittance-type-configurations")]
public class RemittanceTypeConfigurationController(
    IRemittanceTypeConfigurationService configurationService
) : ControllerBase
{
    private readonly IRemittanceTypeConfigurationService _configurationService =
        configurationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateRemittanceTypeConfigurationDto createDto,
        CancellationToken ct
    )
    {
        await _configurationService.CreateAsync(companyId, createDto, ct);
        return Ok();
    }

    [HttpGet("by-remittance-type/{remittanceTypeId:guid}")]
    public async Task<IActionResult> GetByRemittanceTypeId(
        [FromRoute] Guid companyId,
        [FromRoute] Guid remittanceTypeId,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.GetByRemittanceTypeIdAsync(
            companyId,
            remittanceTypeId,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }
}
