using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Domain.Constants;
using NGErp.Base.Service.Authorization;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;

using SharedHCM = NGErp.Shared.Domain.Constants;

namespace NGErp.HCM.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/org-structures")]
[HasPermission(SharedHCM.EntityTypes.OrganizationalStructure, moduleId: ModuleIds.HCM)]
public class OrganizationalStructureController(IOrganizationalStructureService organizationalStructureService) : ControllerBase
{
    private readonly IOrganizationalStructureService _organizationalStructureService = organizationalStructureService;

    [HttpGet("tree")]
    public async Task<IActionResult> GetTreeAtDate(
        [FromRoute] Guid companyId,
        [FromQuery] DateOnly date,
        CancellationToken ct
    )
    {
        var tree = await _organizationalStructureService.GetTreeAtDateAsync(companyId, date, ct);
        return Ok(tree);
    }

    [HttpGet]
    public async Task<IActionResult> GetHistory(
        [FromRoute] Guid companyId,
        [FromQuery] OrganizationalStructureParameters parameters,
        CancellationToken ct
        )
    {
        var result = await _organizationalStructureService.GetAll(companyId, parameters, ct);
        return Ok(result);
    }

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult> Create(
        [FromRoute] Guid companyId,
        [FromQuery] string? description,
        [FromBody] CreateOrganizationStructureDto dto,
        CancellationToken ct
    )
    {
        var result = await _organizationalStructureService.CreateAsync(
            companyId,
            dto,
            ct);

        return Ok(result);
    }
}