using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.Base.Service.Validators;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.RequestFeatures;
using NGErp.Warehouse.Service.RequestValidators.BusinessRulesValidator.Contracts;
using NGErp.Warehouse.Service.RequestValidators.DtoValidators;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptSourceOfSupplyService(
    IAdvancedFilterBuilder filterBuilder,
    IReceiptSourceOfSupplyRepository receiptSourceOfSupplyRepository,
    IReceiptSourceOfSupplyBusinessRuleValidator businessRuleValidator,
    IValidator<CreateReceiptSourceOfSupplyDto> createValidator,
    IValidator<PatchReceiptSourceOfSupplyDto> patchValidator,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptSourceOfSupplyService
{
    private readonly string _key = "ReceiptSourceOfSupply";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IReceiptSourceOfSupplyRepository _receiptSourceOfSupplyRepository = receiptSourceOfSupplyRepository;
    private readonly IReceiptSourceOfSupplyBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateReceiptSourceOfSupplyDto> _createValidator = createValidator;
    private readonly IValidator<PatchReceiptSourceOfSupplyDto> _patchValidator = patchValidator;

    public async Task<ReceiptSourceOfSupplyDto> CreateAsync(
        Guid companyId,
        CreateReceiptSourceOfSupplyDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<ReceiptSourceOfSupply>(createDto);
        entity.CompanyId = companyId;

        var created = await _receiptSourceOfSupplyRepository.AddAsync(entity, ct);

        await _receiptSourceOfSupplyRepository.SaveChangesAsync(ct);
        return _mapper.Map<ReceiptSourceOfSupplyDto>(created);
    }

    public async Task<ReceiptSourceOfSupplyDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<ReceiptSourceOfSupplyDto>(entity);
    }

    public async Task<ListResponseModel<ReceiptSourceOfSupplySlimDto>> FilterByQAsync(
        ReceiptSourceOfSupplyParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _receiptSourceOfSupplyRepository.FilterByQ(parameters);
        var res = await _receiptSourceOfSupplyRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptSourceOfSupplySlimDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptSourceOfSupplySlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ReceiptSourceOfSupplyDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptSourceOfSupplyParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<ReceiptSourceOfSupply>(filterNodeDto);
        var query = _receiptSourceOfSupplyRepository.GetFiltered(companyId, advancedFilters);
        var res = await _receiptSourceOfSupplyRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptSourceOfSupplyDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptSourceOfSupplyDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<ReceiptSourceOfSupplyDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchReceiptSourceOfSupplyDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchReceiptSourceOfSupplyPolicy.Validate(patchDocument);

        var entity = await GetByIdOrThrowAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        var patchDto = _mapper.Map<PatchReceiptSourceOfSupplyDto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {
            errors.Add($"Path: {error.Operation.path}, Error: {error.ErrorMessage}");
        });

        if (errors.Count != 0)
        {
            throw new InvalidPatchDocumentException(errors);
        }

        _mapper.Map(patchDto, entity);

        await _receiptSourceOfSupplyRepository.SaveChangesAsync(ct);
        return _mapper.Map<ReceiptSourceOfSupplyDto>(entity);
    }

    public async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        var entity = await GetByIdOrThrowAsync(
            companyId, id,
            trackChanges: true,
            ct
        );

        _receiptSourceOfSupplyRepository.Remove(entity);
        await _receiptSourceOfSupplyRepository.SaveChangesAsync(ct);
    }

    public Task<int> GetNextCode(Guid companyId, CancellationToken ct)
    {
        return _receiptSourceOfSupplyRepository.GetNextCodeAsync(companyId, ct);
    }

    private async Task<ReceiptSourceOfSupply> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var entity = await _receiptSourceOfSupplyRepository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }
}
