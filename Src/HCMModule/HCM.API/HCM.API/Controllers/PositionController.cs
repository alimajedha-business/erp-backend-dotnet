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
        [FromBody] CreatePositionDto createPositionDto,
        CancellationToken ct
    )
    {
        var positionDto = await _positionService.CreatePositionAsync(
            companyId,
            createPositionDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = positionDto.Id },
            positionDto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
 )
    {
        var category = await _positionService.GetPositionByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(category);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] PositionParameters positionParameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
        )
    {
        var result = await _positionService.GetAllPositionsAsync(
            companyId,
            positionParameters,
            ct,
            filterNodeDto
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
        await _positionService.DeletePositionAsync(companyId, id, ct);
        return Ok();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid companyId,
        Guid id,
        [FromBody] ChangeStatusDto changeStatusDto,
        CancellationToken ct
        )
    {
        await _positionService.ChangeStatusAsync(
            companyId,
            id,
            changeStatusDto.Status,
            ct
            );
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
    [FromRoute] Guid companyId,
    [FromRoute] Guid id,
    [FromBody] JsonPatchDocument<PatchPositionDto> patchDoc,
    CancellationToken ct
)
    {
        var categoryDto = await _positionService.PatchPositionAsync(
            companyId,
            id,
            patchDoc,
            ct
        );

        return Ok(categoryDto);
    }
}