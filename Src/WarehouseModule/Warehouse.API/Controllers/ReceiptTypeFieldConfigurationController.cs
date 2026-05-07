using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.RequestExamples;
using NGErp.Warehouse.Service.Service.Contracts;

using Swashbuckle.AspNetCore.Filters;

namespace NGErp.Warehouse.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt-types/{receiptTypeId:guid}/field-configurations")]
public class ReceiptTypeFieldConfigurationController(
    IReceiptTypeFieldConfigurationService configurationService
) : ControllerBase
{
    private readonly IReceiptTypeFieldConfigurationService _configurationService =
        configurationService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [SwaggerRequestExample(
        typeof(CreateReceiptTypeFieldConfigurationDto),
        typeof(CreateReceiptTypeFieldConfigurationExample)
    )]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        [FromBody] CreateReceiptTypeFieldConfigurationDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.CreateAsync(
            companyId,
            receiptTypeId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetById),
            new { companyId, receiptTypeId, id = dto.Id },
            dto
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.GetByIdAsync(
            companyId,
            receiptTypeId,
            id,
            trackChanges: false,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    [SwaggerRequestExample(
        typeof(JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto>),
        typeof(ReceiptTypeFieldConfigurationPatchExample)
    )]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchReceiptTypeFieldConfigurationDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _configurationService.PatchAsync(
            companyId,
            receiptTypeId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid receiptTypeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _configurationService.DeleteAsync(companyId, receiptTypeId, id, ct);
        return NoContent();
    }
}
