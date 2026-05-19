using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/remittances")]
public class RemittanceController(IRemittanceService remittanceService) : ControllerBase
{
    private readonly IRemittanceService _remittanceService = remittanceService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateRemittanceDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _remittanceService.CreateAsync(companyId, createDto, ct);

        return CreatedAtAction(nameof(GetById), new { companyId, id = dto.Id }, dto);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] RemittanceParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var remittances = await _remittanceService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(remittances);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _remittanceService.GetByIdAsync(companyId, id, trackChanges: false, ct);
        return Ok(dto);
    }

    [HttpGet("new-number")]
    public async Task<IActionResult> GetNextNumber(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var number = await _remittanceService.GetNextNumber(companyId, ct);
        return Ok(number);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchRemittanceDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _remittanceService.PatchAsync(companyId, id, patchDocument, ct);
        return Ok(dto);
    }

    [HttpPost("{id:guid}/post")]
    public async Task<IActionResult> Post(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _remittanceService.PostAsync(companyId, id, ct);
        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _remittanceService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }
}
