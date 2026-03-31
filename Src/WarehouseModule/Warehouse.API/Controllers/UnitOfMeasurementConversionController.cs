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
[Route("api/v{version:apiVersion}/warehouse/uom-conversions")]
public class UnitOfMeasurementConversionController(
        IUnitOfMeasurementConversionService unitOfMeasurementConversionService
) : ControllerBase
{
    private readonly IUnitOfMeasurementConversionService _unitOfMeasurementConversionService =
        unitOfMeasurementConversionService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromBody] CreateUnitOfMeasurementConversionDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _unitOfMeasurementConversionService.CreateAsync(createDto,ct);
        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.Id },
            dto
        );
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromQuery] UnitOfMeasurementConversionParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _unitOfMeasurementConversionService.GetAllAsync(
            parameters,
            ct,
            filterNodeDto
        );

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _unitOfMeasurementConversionService.GetByIdAsync(id, ct);
        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchUnitOfMeasurementConversionDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _unitOfMeasurementConversionService.PatchAsync(
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _unitOfMeasurementConversionService.DeleteAsync(id, ct);
        return NoContent();
    }
}
