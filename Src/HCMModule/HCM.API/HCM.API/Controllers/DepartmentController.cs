using System.Linq.Expressions;

using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.HCM.Domain.Entities;
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
        [FromBody] CreateDepartmentDto createDto,
        CancellationToken ct
        )
    {
        var dto = await _departmentService.CreateAsync(
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
        var dto = await _departmentService.GetByIdAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] DepartmentParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
        )
    {
        var result = await _departmentService.GetAllAsync(
            companyId,
            parameters,
            filterNodeDto ?? new FilterNodeDto(),
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
        await _departmentService.DeleteAsync(companyId, id, ct);
        return Ok();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid companyId,
        Guid id,
        [FromBody] DepartmentChangeStatusDto changeStatusDto,
        CancellationToken ct
        )
    {
        await _departmentService.ChangeStatusAsync(
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
        [FromBody] JsonPatchDocument<PatchDepartmentDto> patchDocument,
        CancellationToken ct
        )
    {
        var dto = await _departmentService.PatchAsync(
            companyId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }
}