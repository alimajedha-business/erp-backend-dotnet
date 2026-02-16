using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.HCM.Service.Services;

namespace NGErp.HCM.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/org-structures")]
public class OrganizationalStructureController(IOrganizationalStructureService organizationalStructureService) : ControllerBase
{
    private readonly IOrganizationalStructureService _organizationalStructureService = organizationalStructureService;

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentTree(
        [FromRoute] Guid companyId,
        CancellationToken ct
    )
    {
        var tree = await _organizationalStructureService.GetTreeAtDateAsync(companyId, new DateOnly(2026, 2, 14));
        return Ok(tree);
    }
}