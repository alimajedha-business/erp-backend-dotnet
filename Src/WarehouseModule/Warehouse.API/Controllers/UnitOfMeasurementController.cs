using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Services;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/uoms")]
public class UnitOfMeasurementController(
    IUnitOfMeasurementService unitOfMeasurementService
) : ControllerBase
{
    private readonly IUnitOfMeasurementService _unitOfMeasurementService = 
        unitOfMeasurementService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateUnitOfMeasurementDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _unitOfMeasurementService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] UnitOfMeasurementParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _unitOfMeasurementService.GetAllAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] UnitOfMeasurementParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _unitOfMeasurementService.GetAllAsync(
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _unitOfMeasurementService.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var code = await _unitOfMeasurementService.GetNextCode(ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchUnitOfMeasurementDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _unitOfMeasurementService.PatchAsync(
            id,
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
        await _unitOfMeasurementService.DeleteAsync(id, ct);
        return NoContent();
    }
}
