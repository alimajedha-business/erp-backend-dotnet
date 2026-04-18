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
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/category-level-constraint")]
public class CategoryLevelConstraintController(
    ICategoryLevelConstraintService constraintService
) : ControllerBase
{
    private readonly ICategoryLevelConstraintService _constraintService = constraintService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateCategoryLevelConstraintDto),
        typeof(CategoryLevelConstraintCreateRequestExample))
    ]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateCategoryLevelConstraintDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _constraintService.CreateAsync(
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

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryLevelConstraintParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _constraintService.FilterByQAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> GetAdvancedSearch(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryLevelConstraintParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _constraintService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
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
        var dto = await _constraintService.GetByIdAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchCategoryLevelConstraintDto>),
        typeof(CategoryLevelConstraintPatchRequestExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryLevelConstraintDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _constraintService.PatchAsync(
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
        await _constraintService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    [HttpGet("next-level")]
    public async Task<IActionResult> GetNextLevel(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var nextLevel = await _constraintService.GetNextLevelAsync(companyId, ct);
        return Ok(nextLevel);
    }
}
