using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/categories")]
public class CategoryController(
    ICategoryService categoryService,
    ICategoryAttributeRuleService attributeRuleService,
    IItemService itemService,
    IExcelExportService excelExportService
) : ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ICategoryAttributeRuleService _attributeRuleService = attributeRuleService;
    private readonly IItemService _itemService = itemService;
    private readonly IExcelExportService _excelExportService = excelExportService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(typeof(CreateCategoryDto), typeof(CreateCategoryExample))]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateCategoryDto createDto,
        CancellationToken ct
    )
    {
        if (createDto.ParentCategoryId is not null)
        {
            await _categoryService.GetByIdAsync(
                companyId,
                createDto.ParentCategoryId.Value,
                trackChanges: false,
                ct
            );
        }

        var dto = await _categoryService.CreateAsync(
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
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _categoryService.FilterByQAsync(
            companyId,
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(CategoryAdvancedSearchExample))]
    public async Task<IActionResult> GetAdvancedSearch(
        [FromRoute] Guid companyId,
        [FromQuery] CategoryParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _categoryService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPost("excel")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(CategoryAdvancedSearchExample))]
    public async Task<IActionResult> ExportToExcel(
        [FromRoute] Guid companyId,
        [FromBody] FilterNodeDto? filterNodeDto,
        [FromQuery] string? columns,
        CancellationToken ct
    )
    {
        var parameters = new CategoryParameters
        {
            Paginated = false,
        };

        var result = await _categoryService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        var columnsList = string.IsNullOrWhiteSpace(columns)
            ? []
            : columns
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

        var excludedColumns = new List<string> { "Id" };

        var fileBytes = _excelExportService.ExportToExcel(
            result.Results,
            columnsList,
            excludedColumns
        );

        Response.Headers.Append("Content-Disposition", "attachment; filename=\"categories.xlsx\"");
        return File(fileBytes.FileContents, fileBytes.ContentType);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _categoryService.GetByIdAsync(
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
        typeof(JsonPatchDocument<PatchCategoryDto>),
        typeof(CategoryPatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _categoryService.PatchAsync(
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
        await _categoryService.DeleteAsync(companyId, id, ct);
        return NoContent();
    }

    #region Items

    [HttpPost("{categoryId:guid}/items")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(typeof(CreateItemDto), typeof(CreateItemExample))]
    public async Task<IActionResult> CreateItem(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromBody] CreateItemDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _itemService.CreateAsync(
            companyId,
            categoryId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetItemById),
            new { companyId, categoryId, id = dto.Id },
            dto
        );
    }

    [HttpPost("{categoryId:guid}/items/list")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(ItemAdvancedSearchExample))]
    [ProducesResponseType(typeof(ListResponseModel<ItemListDto>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetItemsListExample))]
    public async Task<IActionResult> GetItems(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromQuery] ItemParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _itemService.GetCategoryAllItemsAsync(
            companyId,
            categoryId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPost("{categoryId:guid}/items/excel")]
    [SkipModelValidation]
    [SwaggerRequestExample(typeof(object), typeof(ItemAdvancedSearchExample))]
    public async Task<IActionResult> ExportItemsToExcel(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromBody] FilterNodeDto? filterNodeDto,
        [FromQuery] string? columns,
        CancellationToken ct
    )
    {
        var parameters = new ItemParameters
        {
            Paginated = false,
        };

        var result = await _itemService.GetCategoryAllItemsAsync(
            companyId,
            categoryId,
            parameters,
            filterNodeDto,
            ct
        );

        var columnsList = string.IsNullOrWhiteSpace(columns)
            ? []
            : columns
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList();

        var excludedColumns = new List<string> { "Id" };

        var fileBytes = _excelExportService.ExportToExcel(
            result.Results,
            columnsList,
            excludedColumns
        );

        Response.Headers.Append("Content-Disposition", "attachment; filename=\"items.xlsx\"");
        return File(fileBytes.FileContents, fileBytes.ContentType);
    }

    [HttpGet("{categoryId:guid}/items/{id:guid}")]
    public async Task<IActionResult> GetItemById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _itemService.GetByIdAsync(
            companyId,
            categoryId,
            id,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{categoryId:guid}/items/{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchItemDto>),
        typeof(ItemPatchExample)
    )]
    public async Task<IActionResult> PatchItem(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchItemDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _itemService.PatchAsync(
            companyId,
            categoryId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{categoryId:guid}/items/{id:guid}")]
    public async Task<IActionResult> DeleteItem(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _itemService.DeleteAsync(companyId, categoryId, id, ct);
        return NoContent();
    }

    #endregion

    #region AttributeRules

    [HttpPost("{categoryId:guid}/attribute-rules")]
    public async Task<IActionResult> CreateCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromBody] CreateCategoryAttributeRuleDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _attributeRuleService.CreateAsync(
            categoryId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetCategoryAttributeRuleById),
            new { companyId, categoryId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{categoryId:guid}/attribute-rules/{id:guid}")]
    public async Task<IActionResult> GetCategoryAttributeRuleById(
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _attributeRuleService.GetByIdAsync(
            categoryId,
            id,
            trackChanges: false,
            ct
        );
        return Ok(dto);
    }

    [HttpPatch("{categoryId:guid}/attribute-rules/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchCategoryAttributeRule(
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchCategoryAttributeRuleDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _attributeRuleService.PatchAsync(
            categoryId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{categoryId:guid}/attribute-rules/{id:guid}")]
    public async Task<IActionResult> DeleteCategoryAttributeRule(
        [FromRoute] Guid companyId,
        [FromRoute] Guid categoryId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _attributeRuleService.DeleteAsync(categoryId, id, ct);
        return NoContent();
    }

    #endregion
}
