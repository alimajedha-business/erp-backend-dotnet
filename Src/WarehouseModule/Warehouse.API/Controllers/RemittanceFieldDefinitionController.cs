using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/remittance-field-definitions")]
public class RemittanceFieldDefinitionController(
    IRemittanceFieldDefinitionService fieldDefinitionService
) : ControllerBase
{
    private readonly IRemittanceFieldDefinitionService _fieldDefinitionService = fieldDefinitionService;

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] RemittanceFieldDefinitionParameters parameters,
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
    [InherentlyAction(ActionType.Read)]
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
