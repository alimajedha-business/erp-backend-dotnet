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
[Route("api/v{version:apiVersion}/warehouse/shipping-companies")]
public class ShippingCompanyController(
    IShippingCompanyService shippingCompanyService
) : ControllerBase
{
    private readonly IShippingCompanyService _shippingCompanyService = shippingCompanyService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateShippingCompanyDto),
        typeof(CreateShippingCompanyExample)
    )]
    public async Task<IActionResult> Create(
        [FromBody] CreateShippingCompanyDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _shippingCompanyService.CreateAsync(createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> Get(
        [FromQuery] ShippingCompanyParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _shippingCompanyService.GetAllAsync(parameters, ct);
        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(ShippingCompanyAdvancedSearchExample))]
    public async Task<IActionResult> Get(
        [FromQuery] ShippingCompanyParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var shippingCompanies = await _shippingCompanyService.GetAllAsync(
            parameters,
            filterNodeDto ?? new FilterNodeDto(),
            ct
        );

        return Ok(shippingCompanies);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _shippingCompanyService.GetByIdAsync(id, trackChanges: false, ct);
        return Ok(dto);
    }

    [HttpGet("new-code")]
    public async Task<IActionResult> GetNextCode(
        CancellationToken ct
    )
    {
        var code = await _shippingCompanyService.GetNextCode(ct);
        return Ok(code);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchShippingCompanyDto>),
        typeof(ShippingCompanyPatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchShippingCompanyDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _shippingCompanyService.PatchAsync(
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
        await _shippingCompanyService.DeleteAsync(id, ct);
        return NoContent();
    }
}
