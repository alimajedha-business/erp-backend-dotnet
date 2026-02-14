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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/departments")]
public class DepartmentController(
    IDepartmentService departmentService
    ) : ControllerBase
{
    private readonly IDepartmentService _departmentService = departmentService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateDepartmentDto createDepartmentDto,
        CancellationToken ct
    )
    {
        var departmentDto = await _departmentService.CreateDepartmentAsync(
            companyId,
            createDepartmentDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = departmentDto.Id },
            departmentDto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
 )
    {
        var category = await _departmentService.GetDepartmentByIdAsync(
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
        [FromQuery] DepartmentParameters departmentParameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
        )
    {
        var result = await _departmentService.GetAllDepartmentsAsync(
            companyId,
            departmentParameters,
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
        await _departmentService.DeleteDepartmentAsync(companyId, id, ct);
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
        await _departmentService.ChangeStatusAsync(
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
        [FromBody] JsonPatchDocument<PatchDepartmentDto> patchDoc,
        CancellationToken ct
    )
    {
        var departmentDto = await _departmentService.PatchDepartmentAsync(
            companyId,
            id,
            patchDoc,
            ct
        );

        return Ok(departmentDto);
    }
}