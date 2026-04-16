using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;

namespace NGErp.HCM.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/positions")]
public class PositionController(
    IPositionService positionService
    ) : ControllerBase
{
    private readonly IPositionService _positionService = positionService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreatePositionDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _positionService.CreateAsync(
            companyId,
            createDto,
            ct
        );

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
        var dto = await _positionService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] PositionParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
        )
    {
        var result = await _positionService.GetAllAsync(
            companyId,
            parameters,
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
        await _positionService.DeleteAsync(companyId, id, ct);
        return Ok();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid companyId,
        Guid id,
        [FromBody] PositionChangeStatusDto changeStatusDto,
        CancellationToken ct
        )
    {
        await _positionService.ChangeStatusAsync(
            companyId,
            id,
            changeStatusDto,
            ct
            );
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
    [FromRoute] Guid companyId,
    [FromRoute] Guid id,
    [FromBody] JsonPatchDocument<PatchPositionDto> patchDocument,
    CancellationToken ct
)
    {
        var dto = await _positionService.PatchAsync(
            companyId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }
}