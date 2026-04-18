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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/employment-groups")]
public class EmploymentGroupController(
    IEmploymentGroupService employmentGroupService
    ) : ControllerBase
{
    private readonly IEmploymentGroupService _employmentGroupService = employmentGroupService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
    [FromRoute] Guid companyId,
    [FromBody] CreateEmploymentGroupDto createDto,
    CancellationToken ct
    )
    {
        var dto = await _employmentGroupService.CreateAsync(
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
        var dto = await _employmentGroupService.GetByIdAsync(
            companyId,
            id,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] EmploymentGroupParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
        )
    {
        var result = await _employmentGroupService.GetAllAsync(
            companyId,
            parameters,
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
        await _employmentGroupService.DeleteAsync(companyId, id, ct);
        return Ok();
    }

    [HttpPut("{id:guid}")]
    [Consumes("application/json")]
    public async Task<IActionResult> Put(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] UpdateEmploymentGroupDto updateDto,
        CancellationToken ct
        )
    {
        var dto = await _employmentGroupService.UpdateAsync(
            companyId,
            id,
            updateDto,
            ct
        );

        return Ok(dto);
    }
}