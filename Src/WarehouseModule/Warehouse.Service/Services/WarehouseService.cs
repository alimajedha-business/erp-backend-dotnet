using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Accounting.Service.Services;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Service.Contracts;
using NGErp.Accounting.Service.DTOs;

namespace NGErp.Warehouse.Service.Services;

public class WarehouseService(
    IAdvancedFilterBuilder filterBuilder,
    IWarehouseRepository warehouseRepository,
    IPythonIntegrationService pythonService,
    IWarehouseBusinessRuleValidator businessRuleValidator,
    IValidator<CreateWarehouseDto> createValidator,
    IValidator<PatchWarehouseDto> patchValidator,
    IMapper mapper
) : IWarehouseService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IWarehouseRepository _warehouseRepository = warehouseRepository;
    private readonly IPythonIntegrationService _pythonService = pythonService;
    private readonly IWarehouseBusinessRuleValidator _businessRuleValidator =
        businessRuleValidator;
    private readonly IValidator<CreateWarehouseDto> _createValidator = createValidator;
    private readonly IValidator<PatchWarehouseDto> _patchValidator = patchValidator;

    public async Task<WarehouseDto> CreateAsync(
        Guid companyId,
        CreateWarehouseDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<Domain.Entities.Warehouse>(createDto);
        entity.CompanyId = companyId;

        var created = await _warehouseRepository.AddAsync(entity, ct);

        await _warehouseRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseDto>(created);
    }

    public async Task<WarehouseDto> GetByIdAsync(
        Guid companyId,
        Guid ledgerId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var warehouse = await _warehouseRepository.SingleOrDefaultAsync(
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            mapperConfig: _mapper.ConfigurationProvider,
            trackChanges,
            ct
        ) ?? throw new WarehouseNotFoundException();

        if (warehouse.WarehouseSlaveAccountCompanyId is not null)
        {
            var accountCompany = await _pythonService
                .GetSlaveAccountCompanyByIdAsync(
                    companyId,
                    ledgerId,
                    warehouse.WarehouseSlaveAccountCompanyId
                ) ?? throw new WarehouseNotFoundException();

            warehouse.WarehouseSlaveAccountCompany = accountCompany;
        }

        if (warehouse.ReturnFromPurchaseSlaveAccountCompanyId is not null)
        {
            var accountCompany = await _pythonService
                .GetSlaveAccountCompanyByIdAsync(
                    companyId,
                    ledgerId,
                    warehouse.ReturnFromPurchaseSlaveAccountCompanyId
                ) ?? throw new WarehouseNotFoundException();

            warehouse.ReturnFromPurchaseSlaveAccountCompany = accountCompany;
        }

        return warehouse;
    }

    public async Task<ListResponseModel<WarehouseSlimDto>> FilterByQAsync(
        Guid companyId,
        WarehouseParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _warehouseRepository.FilterByQ(companyId, parameters);
        var res = await _warehouseRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<WarehouseSlimDto>(
            results: _mapper.Map<IReadOnlyList<WarehouseSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<WarehouseListDto>> GetFilteredAsync(
        Guid companyId,
        WarehouseParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<Domain.Entities.Warehouse>(filterNodeDto);
        var query = _warehouseRepository.GetFiltered(companyId, advancedFilters);

        var res = await _warehouseRepository.GetResponseListAsync<
            Domain.Entities.Warehouse,
            WarehouseListDto
        >(query, parameters, _mapper.ConfigurationProvider, ct);

        return new ListResponseModel<WarehouseListDto>(
            results: res.items,
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<WarehouseDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchWarehouseDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchWarehousePolicy.Validate(patchDocument);

        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchWarehouseDto.Code)
        );

        var warehouse = await _warehouseRepository.SingleOrDefaultAsync(
            predicate: p =>
                p.CompanyId == companyId &&
                p.Id == id,
            trackChanges: true,
            ct
        ) ?? throw new WarehouseNotFoundException();

        var patchDto = _mapper.Map<PatchWarehouseDto>(warehouse);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        await RequestBodyValidator.ValidateAsync(_patchValidator, patchDto, ct);

        if (codePatched && patchDto.Code.HasValue)
        {
            await _businessRuleValidator.ValidateWarehouseCodeUniquenessAsync(
                companyId,
                excludedWarehouseId: id,
                patchDto.Code.Value,
                ct
            );
        }

        _businessRuleValidator.ValidateAccountingRules(patchDto);

        _mapper.Map(patchDto, warehouse);
        ApplyRemovedNullableFields(patchDocument, warehouse);

        await _warehouseRepository.SaveChangesAsync(ct);
        return _mapper.Map<WarehouseDto>(warehouse);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);

        await _warehouseRepository.Remove(e =>
            e.CompanyId == companyId &&
            e.Id == id,
            ct
        );
    }

    public Task<int> GetNextCode(
        Guid companyId,
        CancellationToken ct
    )
    {
        return _warehouseRepository.GetNextCodeAsync(companyId, ct);
    }

    private static void ApplyRemovedNullableFields(
        JsonPatchDocument<PatchWarehouseDto> patchDocument,
        Domain.Entities.Warehouse warehouse
    )
    {
        var removedPaths = patchDocument.Operations
            .Where(operation => operation.op.Equals("remove", StringComparison.OrdinalIgnoreCase))
            .Select(operation => operation.path.Trim('/'))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (removedPaths.Contains(nameof(PatchWarehouseDto.MaxMonetaryValue)))
            warehouse.MaxMonetaryValue = default;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.WarehouseSlaveAccountCompanyId)))
            warehouse.WarehouseSlaveAccountCompanyId = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.WarehouseAccountMasterValue)))
            warehouse.WarehouseAccountMasterValue = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.WarehouseAccountSlaveValue)))
            warehouse.WarehouseAccountSlaveValue = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.WarehouseAccountDetailed1Value)))
            warehouse.WarehouseAccountDetailed1Value = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.WarehouseAccountDetailed2Value)))
            warehouse.WarehouseAccountDetailed2Value = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.ReturnFromPurchaseSlaveAccountCompanyId)))
            warehouse.ReturnFromPurchaseSlaveAccountCompanyId = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.ReturnFromPurchaseAccountMasterValue)))
            warehouse.ReturnFromPurchaseAccountMasterValue = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.ReturnFromPurchaseAccountSlaveValue)))
            warehouse.ReturnFromPurchaseAccountSlaveValue = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.ReturnFromPurchaseAccountDetailed1Value)))
            warehouse.ReturnFromPurchaseAccountDetailed1Value = null;

        if (removedPaths.Contains(nameof(PatchWarehouseDto.ReturnFromPurchaseAccountDetailed2Value)))
            warehouse.ReturnFromPurchaseAccountDetailed2Value = null;
    }
}


