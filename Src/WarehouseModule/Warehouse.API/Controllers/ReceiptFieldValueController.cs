using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Warehouse.Domain.Constants;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-warehouse")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/warehouse/receipt/field-enitity-values")]
[HasPermission(EntityTypes.Receipt)]
public class ReceiptFieldValueController(
    IReceiptFieldValueService fieldValueService
) : ControllerBase
{
    private readonly IReceiptFieldValueService _fieldValueService = fieldValueService;

    [HttpGet("{reference:int}/filter-by-q")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromRoute] ReceiptReferenceEntityType reference,
        [FromQuery] AttributeParameters parameters,
        CancellationToken ct
    )
    {
        var result = await _fieldValueService.FilterByQAsync(
            companyId,
            reference,
            parameters,
            ct
        );

        return Ok(result);
    }
}
