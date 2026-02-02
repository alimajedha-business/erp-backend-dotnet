using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.RequestFeatures;
using NGErp.HCM.Domain.Entities;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;


namespace NGErp.HCM.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/{companyId:guid}/hcm/departments")]
//[JwtAuthorize]
public class DepartmentController(
    IDepartmentService departmentService,
    IAdvancedFilterBuilder filterBuilder) : ControllerBase
{
    private readonly IDepartmentService _departmentService = departmentService;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateDepartmentDto createDepartmentDto,
        CancellationToken ct
    )
    {
        var departmentDto = await _departmentService.CreateDepartmentAsync(
            companyId,
            createDepartmentDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, id = departmentDto.Id },
            departmentDto
        );
    }

    //[HttpGet]
    //public async Task<IActionResult> Get(
    //    [FromRoute] Guid companyId,
    //    [FromQuery] CategoryParameters categoryParameters,
    //    CancellationToken ct
    //)
    //{
    //    var result = await _categoryService.GetAllCategoriesAsync(
    //        companyId,
    //        categoryParameters,
    //        ct
    //    );

    //    return Ok(result);
    //}

    //[HttpPost("search")]
    //[SkipModelValidation]
    //public async Task<IActionResult> GetWithSearch(
    //    [FromRoute] Guid companyId,
    //    [FromQuery] CategoryParameters categoryParameters,
    //    [FromBody] FilterNodeDto? filterNodeDto,
    //    CancellationToken ct
    //)
    //{
    //    var requestAdvancedFilters = _filterBuilder.Build<Category>(filterNodeDto);

    //    var result = await _categoryService.GetAllCategoriesAsync(
    //        companyId,
    //        categoryParameters,
    //        ct,
    //        requestAdvancedFilters
    //    );

    //    return Ok(result);
    //}

    //[HttpGet("{id:guid}")]
    //public async Task<IActionResult> GetById(
    //    [FromRoute] Guid companyId,
    //    [FromRoute] Guid id,
    //    CancellationToken ct
    //)
    //{
    //    var category = await _categoryService.GetCategoryByIdAsync(
    //        companyId,
    //        id,
    //        ct
    //    );

    //    return Ok(category);
    //}

    //[HttpGet("{id:guid}/children")]
    //public async Task<IActionResult> GetChildren(
    //    [FromRoute] Guid companyId,
    //    [FromRoute] Guid id,
    //    [FromQuery] CategoryParameters categoryParameters,
    //    CancellationToken ct
    //)
    //{
    //    var result = await _categoryService.GetDirectCategoryChildrenByIdAsync(
    //        companyId,
    //        id,
    //        categoryParameters,
    //        ct
    //    );

    //    return Ok(result);
    //}

    //[HttpPatch("{id:guid}")]
    //public async Task<IActionResult> Update(
    //    [FromRoute] Guid companyId,
    //    [FromRoute] Guid id,
    //    [FromBody] UpdateCategoryDto updateCategoryDto,
    //    CancellationToken ct
    //)
    //{
    //    var categoryDto = await _categoryService.UpdateCategoryAsync(
    //        companyId,
    //        id,
    //        updateCategoryDto,
    //        ct
    //    );

    //    return Ok(categoryDto);
    //}

    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> Delete(
    //    [FromRoute] Guid companyId,
    //    [FromRoute] Guid id,
    //    CancellationToken ct
    //)
    //{
    //    await _categoryService.DeleteCategoryAsync(companyId, id, ct);
    //    return Ok();
    //}


}
