using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;

namespace NGErp.HCM.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/hcm/military-service-statuses")]
[HasPermission("MILITARY_SERVICE_STATUS")]
public class MilitaryServiceStatusController(
    IMilitaryServiceStatusService militaryServiceStatusService
) : ControllerBase
{
    private readonly IMilitaryServiceStatusService _militaryServiceStatusService = militaryServiceStatusService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromBody] CreateMilitaryServiceStatusDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _militaryServiceStatusService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new {id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _militaryServiceStatusService.GetByIdAsync(id, false, ct);
        return Ok(dto);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromQuery] MilitaryServiceStatusParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _militaryServiceStatusService.GetFilteredAsync(
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchMilitaryServiceStatusDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _militaryServiceStatusService.PatchAsync(id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _militaryServiceStatusService.DeleteAsync(id, ct);
        return NoContent();
    }
}
