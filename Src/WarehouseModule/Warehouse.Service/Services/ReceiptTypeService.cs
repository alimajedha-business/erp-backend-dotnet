using System.Linq.Expressions;

using AutoMapper;

using FluentValidation;

using Microsoft.AspNetCore.JsonPatch;

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
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptTypeService(
    IAdvancedFilterBuilder filterBuilder,
    IReceiptTypeRepository receiptRepository,
    IReceiptTypeBusinessRuleValidator businessRuleValidator,
    IValidator<CreateReceiptTypeDto> createValidator,
    IValidator<PatchReceiptTypeDto> patchValidator,
    IMapper mapper
) : IReceiptTypeService
{
    private readonly IMapper _mapper = mapper;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly IReceiptTypeRepository _receiptRepository = receiptRepository;
    private readonly IReceiptTypeBusinessRuleValidator _businessRuleValidator = businessRuleValidator;
    private readonly IValidator<CreateReceiptTypeDto> _createValidator = createValidator;
    private readonly IValidator<PatchReceiptTypeDto> _patchValidator = patchValidator;

    public async Task<ReceiptTypeDto> CreateAsync(
        Guid companyId,
        CreateReceiptTypeDto createDto,
        CancellationToken ct
    )
    {
        RequestBodyValidator.ThrowIfNull(createDto);
        await RequestBodyValidator.ValidateAsync(_createValidator, createDto, ct);
        await _businessRuleValidator.ValidateCreateAsync(companyId, createDto, ct);

        var entity = _mapper.Map<ReceiptType>(createDto);
        entity.CompanyId = companyId;

        var created = await _receiptRepository.AddAsync(entity, ct);

        await _receiptRepository.SaveChangesAsync(ct);
        return _mapper.Map<ReceiptTypeDto>(created);
    }

    public async Task<ReceiptTypeDto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var receipt = await GetSingleOrThrowAsync(
            trackChanges: trackChanges,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        return _mapper.Map<ReceiptTypeDto>(receipt);
    }

    public async Task<ListResponseModel<ReceiptTypeSlimDto>> FilterByQAsync(
        Guid companyId,
        ReceiptTypeParameters parameters,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var query = _receiptRepository
            .FilterByQ(companyId, parameters)
            .Where(e => e.ReceiptTypeConfiguration != null);

        var res = await _receiptRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptTypeSlimDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptTypeSlimDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public async Task<ListResponseModel<ReceiptTypeDto>> GetFilteredAsync(
        Guid companyId,
        ReceiptTypeParameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {
        _businessRuleValidator.ValidateParameters(parameters);

        var advancedFilters = _filterBuilder.Build<ReceiptType>(filterNodeDto);
        var query = _receiptRepository.GetFiltered(companyId, advancedFilters);
        var res = await _receiptRepository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<ReceiptTypeDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptTypeDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }

    public virtual async Task<ReceiptTypeDto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<PatchReceiptTypeDto> patchDocument,
        CancellationToken ct
    )
    {
        PatchReceiptTypePolicy.Validate(patchDocument);

        var codePatched = PatchPolicyValidator.HasProperty(
            patchDocument,
            nameof(PatchReceiptTypeDto.Code)
        );

        var receipt = await GetSingleOrThrowAsync(
            trackChanges: true,
            predicate: p => p.CompanyId == companyId && p.Id == id,
            ct
        );

        var patchDto = _mapper.Map<PatchReceiptTypeDto>(receipt);
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
            await _businessRuleValidator.ValidateReceiptTypeCodeUniquenessAsync(
                companyId,
                excludedReceiptId: id,
                patchDto.Code.Value,
                ct
            );
        }

        _mapper.Map(patchDto, receipt);

        await _receiptRepository.SaveChangesAsync(ct);
        return _mapper.Map<ReceiptTypeDto>(receipt);
    }

    public virtual async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {
        await _businessRuleValidator.ValidateDeleteAsync(companyId, id, ct);
        await _receiptRepository.Remove(
            e =>
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
        return _receiptRepository.GetNextCodeAsync(companyId, ct);
    }

    public async Task<ReceiptType> GetSingleOrThrowAsync(
        bool trackChanges,
        Expression<Func<ReceiptType, bool>> predicate,
        CancellationToken ct = default
    )
    {
        var entity = await _receiptRepository.SingleOrDefaultAsync(
            predicate,
            trackChanges,
            ct
        );

        return entity ?? throw new ReceiptTypeNotFoundException();
    }
}
