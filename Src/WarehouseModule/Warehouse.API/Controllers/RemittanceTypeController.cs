using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/remittance-types")]
public class RemittanceTypeController(
    IRemittanceTypeService remittanceTypeService
) : ControllerBase
{
    private readonly IRemittanceTypeService _remittanceTypeService = remittanceTypeService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateRemittanceTypeDto),
        typeof(CreateRemittanceTypeExample)
    )]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateRemittanceTypeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _remittanceTypeService.CreateAsync(
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

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> GetByQ(
        [FromRoute] Guid companyId,
        [FromQuery] RemittanceTypeParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _remittanceTypeService.FilterByQAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(RemittanceTypeAdvancedSearchExample))]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] RemittanceTypeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var remittanceTypes = await _remittanceTypeService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(remittanceTypes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _remittanceTypeService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var code = await _remittanceTypeService.GetNextCode(companyId, ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchRemittanceTypeDto>),
        typeof(RemittanceTypePatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchRemittanceTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _remittanceTypeService.PatchAsync(
            companyId,
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
        await _remittanceTypeService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }
}
