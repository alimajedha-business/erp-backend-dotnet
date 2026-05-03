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
[Route("api/v{version:apiVersion}/hcm/education-fields")]
public class EducationFieldController(
    IEducationFieldService educationFieldService
) : ControllerBase
{
    private readonly IEducationFieldService _educationFieldService = educationFieldService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromBody] CreateEducationFieldDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _educationFieldService.CreateAsync(createDto, ct);

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
        var dto = await _educationFieldService.GetByIdAsync(id, false, ct);
        return Ok(dto);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromQuery] EducationFieldParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _educationFieldService.GetFilteredAsync(
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
        [FromBody] JsonPatchDocument<PatchEducationFieldDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _educationFieldService.PatchAsync(id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _educationFieldService.DeleteAsync(id, ct);
        return NoContent();
    }
}
