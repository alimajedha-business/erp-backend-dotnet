using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Domain.Constants;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.HCM.Domain.Constants;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Service.Contracts;

namespace NGErp.HCM.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/position-jobs")]
[HasPermission(EntityTypes.PositionJob, moduleId: ModuleIds.HCM)]
public class PositionJobController(
    IPositionJobService positionJobService
) : ControllerBase
{
    private readonly IPositionJobService _positionJobService = positionJobService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreatePositionJobDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _positionJobService.CreateAsync(companyId, createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _positionJobService.GetByIdAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] PositionJobParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _positionJobService.GetFilteredAsync(
            companyId, parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _positionJobService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchPositionJobDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _positionJobService.PatchAsync(
            companyId, id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }
}
