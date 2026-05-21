using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Domain.Constants;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;

using GeneralHCM = NGErp.General.Domain.Constants;

namespace NGErp.HCM.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/hcm/relative-types")]
[HasPermission(GeneralHCM.EntityTypes.RelativeType, moduleId: ModuleIds.HCM)]
public class RelativeTypeController(
    IRelativeTypeService relativeTypeService
) : ControllerBase
{
    private readonly IRelativeTypeService _relativeTypeService = relativeTypeService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromBody] CreateRelativeTypeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _relativeTypeService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _relativeTypeService.GetByIdAsync(id, false, ct);
        return Ok(dto);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromQuery] RelativeTypeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _relativeTypeService.GetFilteredAsync(
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
        [FromBody] JsonPatchDocument<PatchRelativeTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _relativeTypeService.PatchAsync(id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _relativeTypeService.DeleteAsync(id, ct);
        return NoContent();
    }
}
