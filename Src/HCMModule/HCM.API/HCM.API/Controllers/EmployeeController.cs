using Asp.Versioning;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.Authorization;
using NGErp.Base.Service.DTOs;
using NGErp.HCM.Service.DTOs;
using NGErp.HCM.Service.RequestFeatures;
using NGErp.HCM.Service.Services;

namespace NGErp.HCM.API.Controllers;

[JwtAuthorize]
[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-hcm")]
[Route("api/v{version:apiVersion}/companies/{companyId:guid}/hcm/employees")]
[HasPermission("EMPLOYEE")]
public class EmployeeController(
    IEmployeeService employeeService,
    IEmployeeEducationService employeeEducationService,
    IEmployeeWorkExperienceService employeeWorkExperienceService,
    IEmployeeWarriorRecordService employeeWarriorRecordService,
    IEmployeeRelativeService employeeRelativeService,
    IEmployeeDependantService employeeDependantService
) : ControllerBase
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IEmployeeEducationService _employeeEducationService = employeeEducationService;
    private readonly IEmployeeWorkExperienceService _employeeWorkExperienceService = employeeWorkExperienceService;
    private readonly IEmployeeWarriorRecordService _employeeWarriorRecordService = employeeWarriorRecordService;
    private readonly IEmployeeRelativeService _employeeRelativeService = employeeRelativeService;
    private readonly IEmployeeDependantService _employeeDependantService = employeeDependantService;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] CreateEmployeeDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _employeeService.CreateAsync(
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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _employeeService.GetByIdAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPost("list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] EmployeeParamaters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _employeeService.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _employeeService.DeleteAsync(companyId, id, ct);
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _employeeService.PatchAsync(
            companyId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    #region Educations

    [HttpPost("{employeeId:guid}/educations")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateEducation(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromBody] CreateEmployeeEducationDto createDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeEducationService.CreateAsync(
            employeeId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetEducationById),
            new { companyId, employeeId, id = dto.Id },
            dto
        );
    }

    [HttpPost("{employeeId:guid}/educations/list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> GetEducations(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromQuery] EmployeeEducationParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var result = await _employeeEducationService.GetFilteredAsync(
            employeeId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{employeeId:guid}/educations/{id:guid}")]
    public async Task<IActionResult> GetEducationById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeEducationService.GetByIdAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{employeeId:guid}/educations/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchEducation(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeEducationDto> patchDocument,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeEducationService.PatchAsync(
            employeeId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{employeeId:guid}/educations/{id:guid}")]
    public async Task<IActionResult> DeleteEducation(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        await _employeeEducationService.DeleteAsync(employeeId, id, ct);
        return NoContent();
    }

    #endregion

    #region Work Experiences

    [HttpPost("{employeeId:guid}/work-experiences")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateWorkExperience(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromBody] CreateEmployeeWorkExperienceDto createDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeWorkExperienceService.CreateAsync(
            employeeId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetWorkExperienceById),
            new { companyId, employeeId, id = dto.Id },
            dto
        );
    }

    [HttpPost("{employeeId:guid}/work-experiences/list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> GetWorkExperiences(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromQuery] EmployeeWorkExperienceParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var result = await _employeeWorkExperienceService.GetFilteredAsync(
            employeeId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{employeeId:guid}/work-experiences/{id:guid}")]
    public async Task<IActionResult> GetWorkExperienceById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeWorkExperienceService.GetByIdAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{employeeId:guid}/work-experiences/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchWorkExperience(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeWorkExperienceDto> patchDocument,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeWorkExperienceService.PatchAsync(
            employeeId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{employeeId:guid}/work-experiences/{id:guid}")]
    public async Task<IActionResult> DeleteWorkExperience(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        await _employeeWorkExperienceService.DeleteAsync(employeeId, id, ct);
        return NoContent();
    }

    #endregion

    #region Warrior Records

    [HttpPost("{employeeId:guid}/warrior-records")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateWarriorRecord(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromBody] CreateEmployeeWarriorRecordDto createDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeWarriorRecordService.CreateAsync(
            employeeId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetWarriorRecordById),
            new { companyId, employeeId, id = dto.Id },
            dto
        );
    }

    [HttpPost("{employeeId:guid}/warrior-records/list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> GetWarriorRecords(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromQuery] EmployeeWarriorRecordParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var result = await _employeeWarriorRecordService.GetFilteredAsync(
            employeeId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{employeeId:guid}/warrior-records/{id:guid}")]
    public async Task<IActionResult> GetWarriorRecordById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeWarriorRecordService.GetByIdAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{employeeId:guid}/warrior-records/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchWarriorRecord(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeWarriorRecordDto> patchDocument,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeWarriorRecordService.PatchAsync(
            employeeId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{employeeId:guid}/warrior-records/{id:guid}")]
    public async Task<IActionResult> DeleteWarriorRecord(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        await _employeeWarriorRecordService.DeleteAsync(employeeId, id, ct);
        return Ok();
    }

    #endregion

    #region Relatives

    [HttpPost("{employeeId:guid}/relatives")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateRelative(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromBody] CreateEmployeeRelativeDto createDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeRelativeService.CreateAsync(
            employeeId,
            createDto,
            ct
        );

        return CreatedAtAction(
            nameof(GetRelativeById),
            new { companyId, employeeId, id = dto.Id },
            dto
        );
    }

    [HttpPost("{employeeId:guid}/relatives/list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> GetRelatives(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromQuery] EmployeeRelativeParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var result = await _employeeRelativeService.GetFilteredAsync(
            employeeId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{employeeId:guid}/relatives/{id:guid}")]
    public async Task<IActionResult> GetRelativeById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeRelativeService.GetByIdAsync(
            employeeId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("{employeeId:guid}/relatives/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchRelative(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeRelativeDto> patchDocument,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        var dto = await _employeeRelativeService.PatchAsync(
            employeeId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("{employeeId:guid}/relatives/{id:guid}")]
    public async Task<IActionResult> DeleteRelative(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await EnsureEmployeeExistsAsync(companyId, employeeId, ct);

        await _employeeRelativeService.DeleteAsync(employeeId, id, ct);
        return NoContent();
    }

    #endregion

    #region Dependants

    [HttpPost("dependants")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> CreateDependant(
        [FromRoute] Guid companyId,
        [FromBody] CreateEmployeeDependantDto createDto,
        CancellationToken ct
    )
    {
        var dto = await _employeeDependantService.CreateAsync(createDto, ct);
        var employeeId = dto.EmployeeRelative.Employee.Id;

        return CreatedAtAction(
            nameof(GetDependantById),
            new { companyId, employeeId, id = dto.Id },
            dto
        );
    }

    [HttpPost("{employeeId:guid}/dependants/list")]
    [InherentlyAction(ActionType.Read)]
    public async Task<IActionResult> GetDependants(
        [FromRoute] Guid companyId,
        [FromRoute] Guid employeeId,
        [FromQuery] EmployeeDependantParameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {
        var result = await _employeeDependantService.GetFilteredAsync(
            employeeId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }

    [HttpGet("{employeeId:guid}/dependants/{id:guid}")]
    public async Task<IActionResult> GetDependantById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        var dto = await _employeeDependantService.GetByIdAsync(
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }

    [HttpPatch("dependants/{id:guid}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> PatchDependant(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<PatchEmployeeDependantDto> patchDocument,
        CancellationToken ct
    )
    {
        var dto = await _employeeDependantService.PatchAsync(
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }

    [HttpDelete("dependants/{id:guid}")]
    public async Task<IActionResult> DeleteDependant(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {
        await _employeeDependantService.DeleteAsync(id, ct);
        return NoContent();
    }

    #endregion

    private async Task EnsureEmployeeExistsAsync(
        Guid companyId,
        Guid employeeId,
        CancellationToken ct
    )
    {
        await _employeeService.GetByIdAsync(
            companyId,
            employeeId,
            trackChanges: false,
            ct
        );
    }
}

