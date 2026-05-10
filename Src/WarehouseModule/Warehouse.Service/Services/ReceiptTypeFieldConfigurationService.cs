using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class ReceiptTypeFieldConfigurationService(
    IReceiptTypeFieldConfigurationRepository configurationRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IReceiptTypeFieldConfigurationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
    private readonly IReceiptTypeFieldConfigurationRepository _configurationRepository =
        configurationRepository;

    public async Task<ReceiptTypeFieldConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            companyId,
            receiptTypeId,
            id,
            trackChanges,
            ct
        );

        return Localize(_mapper.Map<ReceiptTypeFieldConfigurationDto>(configuration));
    }

    private async Task<ReceiptTypeFieldConfiguration> GetSingleOrThrowAsync(
        Guid companyId,
        Guid receiptTypeId,
        Guid id,
        bool trackChanges,
        CancellationToken ct = default
    )
    {
        var entity = await _configurationRepository.SingleOrDefaultAsync(
            e =>
                e.ReceiptTypeConfiguration.ReceiptTypeId == receiptTypeId &&
                e.Id == id,
            trackChanges,
            ct
        );

        return entity ?? throw new ReceiptTypeFieldConfigurationNotFoundException();
    }

    private ReceiptTypeFieldConfigurationDto Localize(ReceiptTypeFieldConfigurationDto dto)
    {
        return dto with
        {
            FieldDefinition = dto.FieldDefinition with
            {
                Title = _localizer[dto.FieldDefinition.Title].Value
            }
        };
    }
}
