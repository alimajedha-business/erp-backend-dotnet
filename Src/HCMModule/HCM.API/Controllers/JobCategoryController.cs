using Asp.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Domain.Constants;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.Services;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Service.Contracts;

using GeneralHCM = NGErp.General.Domain.Constants;

namespace NGErp.HCM.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/hcm/job-categories")]
[HasPermission(GeneralHCM.EntityTypes.JobCategory, moduleId: ModuleIds.HCM)]
public class JobCategoryController(
    IJobCategoryService jobCategoryService,
    IExcelExportService excelExportService
) : ControllerBase
{
    private readonly IJobCategoryService _JobCategoryService = jobCategoryService;
    private readonly IExcelExportService _excelExportService = excelExportService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromBody] CreateJobCategoryDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _JobCategoryService.CreateAsync(
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.Id },
            dto
        );
    }

    [HttpGet("filter-by-q")]
    public async Task<IActionResult> GetByQ( [FromQuery] JobCategoryParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _JobCategoryService.FilterByQAsync(
            parameters,
            ct
        );

        return Ok(result);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromQuery] JobCategoryParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _JobCategoryService.GetFilteredAsync(
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpPost("excel")]
    [InherentlyAction(ActionType.Export)]
    public async Task<IActionResult> ExportToExcel(
        [FromBody] FilterNodeDto? filterNodeDto,
        [FromQuery] string? columns,
        CancellationToken ct
    )
    {
        var parameters = new JobCategoryParameters
        {
            Paginated = false,
        };

        var result = await _JobCategoryService.GetFilteredAsync(
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
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _JobCategoryService.GetByIdAsync(
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchJobCategoryDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _JobCategoryService.PatchAsync(
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
        await _JobCategoryService.DeleteAsync(id, ct);
        return NoContent();
    }
}