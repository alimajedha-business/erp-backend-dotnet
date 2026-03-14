using System.ComponentModel.DataAnnotations;

using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;

namespace NGErp.HCM.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/org-structures")]
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

    [HttpGet()]
    public async Task<IActionResult> GetHistory(
        [FromRoute] Guid companyId,
        [FromQuery] OrganizationalStructureParameters parameters,
        CancellationToken ct
        )
    {
        var result = await _organizationalStructureService.GetAll(companyId, parameters, ct);
        return Ok(result);
    }

    [HttpPost()]
    public ActionResult Save(
        [FromRoute] Guid companyId,
        [FromQuery, Required] DateOnly effectiveDate,
        [FromQuery] string? description,
        [FromBody] CreateOrganizationStructureDto dto,
        CancellationToken ct
        )
    {
        _organizationalStructureService.SaveTreeAsync(
            companyId,
            dto,
            effectiveDate,
            description,
            ct);
        return Ok(dto);
    }
}