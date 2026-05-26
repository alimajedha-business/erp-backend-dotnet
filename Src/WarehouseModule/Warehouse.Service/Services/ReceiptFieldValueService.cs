using AutoMapper;

using NGErp.Base.Service.RequestFeatures;
using NGErp.Base.Service.ResponseModels;
using NGErp.Shared.Domain.Entities;
using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptFieldValueService(
    IReceiptFieldValueRepository fieldValueRepository,
    IMapper mapper
) : IReceiptFieldValueService
{
    private readonly IMapper _mapper = mapper;
    private readonly IReceiptFieldValueRepository _fieldValueRepository = fieldValueRepository;

    public async Task<ListResponseModel<ReceiptFieldValueReferenceDto>> FilterByQAsync(
        Guid companyId,
        ReceiptReferenceEntityType reference,
        RequestParameters parameters,
        CancellationToken ct = default
    )
    {
        ListQueryResult<ReceiptFieldValueReferenceDto>? res = null;
        if (reference == ReceiptReferenceEntityType.Warehouse)
        {
            res = await _fieldValueRepository.FilterByQ<Domain.Entities.Warehouse>(
                parameters,
                _mapper.ConfigurationProvider,
                predicate: p => p.CompanyId == companyId,
                ct
            );
        }

        if (reference == ReceiptReferenceEntityType.SourceOfSupply)
        {
            res = await _fieldValueRepository.FilterByQ<ReceiptSourceOfSupply>(
                parameters,
                _mapper.ConfigurationProvider,
                predicate: p => p.CompanyId == companyId,
                ct
            );
        }

        if (reference == ReceiptReferenceEntityType.CompanyUnit)
        {
            res = await _fieldValueRepository.FilterByQ<CompanyUnit>(
                parameters,
                _mapper.ConfigurationProvider,
                predicate: p => p.CompanyId == companyId,
                ct

            );
        }

        if (res is null)
        {
            throw new ReceiptFieldReferenceNotFoundException();
        }

        return new ListResponseModel<ReceiptFieldValueReferenceDto>(
            results: _mapper.Map<IReadOnlyList<ReceiptFieldValueReferenceDto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }
}
