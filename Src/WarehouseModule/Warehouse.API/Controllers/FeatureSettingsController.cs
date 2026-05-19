using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

//[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/feature-settings")]
public class FeatureSettingsController(
    IFeatureSettingsService featureSettingsService
) : ControllerBase
{
    private readonly IFeatureSettingsService _featureSettingsService = featureSettingsService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateFeatureSettingsDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _featureSettingsService.CreateAsync(companyId, createDto, ct);

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
        var dto = await _featureSettingsService.GetByIdAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchFeatureSettingsDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _featureSettingsService.PatchAsync(
            companyId, id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _featureSettingsService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }
}
