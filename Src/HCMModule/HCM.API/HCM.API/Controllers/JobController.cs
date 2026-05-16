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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/jobs")]
[HasPermission("JOB")]
public class JobController(
    IJobService jobService
) : ControllerBase
{
    private readonly IJobService _jobService = jobService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateJobDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _jobService.CreateAsync(companyId, createDto, ct);

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
        var dto = await _jobService.GetByIdAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetByQ(
        [FromRoute] Guid companyId,
        [FromQuery] JobParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _jobService.FilterByQAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] JobParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _jobService.GetFilteredAsync(
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
        await _jobService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchJobDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _jobService.PatchAsync(
            companyId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }
}
