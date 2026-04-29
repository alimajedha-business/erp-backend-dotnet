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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/employee-educations")]
public class EmployeeEducationController(
    IEmployeeEducationService employeeEducationService
) : ControllerBase
{
    private readonly IEmployeeEducationService _employeeEducationService = employeeEducationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateEmployeeEducationDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _employeeEducationService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new {companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyid,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _employeeEducationService.GetByIdAsync(
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromQuery] EmployeeEducationParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _employeeEducationService.GetFilteredAsync(
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _employeeEducationService.DeleteAsync(id, ct);
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeEducationDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _employeeEducationService.PatchAsync(
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }
}
