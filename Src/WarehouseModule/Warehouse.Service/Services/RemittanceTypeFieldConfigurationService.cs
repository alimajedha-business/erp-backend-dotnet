using AutoMapper;

using Microsoft.Extensions.Localization;

using NGErp.Warehouse.Domain.Entities;
using NGErp.Warehouse.Domain.Exceptions;
using NGErp.Warehouse.Service.DTOs;
using NGErp.Warehouse.Service.Repository.Contracts;
using NGErp.Warehouse.Service.Resources;
using NGErp.Warehouse.Service.Service.Contracts;

namespace NGErp.Warehouse.Service.Services;

public class RemittanceTypeFieldConfigurationService(
    IRemittanceTypeFieldConfigurationRepository configurationRepository,
    IMapper mapper,
    IStringLocalizer<WarehouseResource> localizer
) : IRemittanceTypeFieldConfigurationService
{
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer<WarehouseResource> _localizer = localizer;
    private readonly IRemittanceTypeFieldConfigurationRepository _configurationRepository =
        configurationRepository;

    public async Task<RemittanceTypeFieldConfigurationDto> GetByIdAsync(
        Guid companyId,
        Guid remittanceTypeId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {
        var configuration = await GetSingleOrThrowAsync(
            companyId,
            remittanceTypeId,
            id,
            trackChanges,
            ct
        );

        return Localize(_mapper.Map<RemittanceTypeFieldConfigurationDto>(configuration));
    }

    private async Task<RemittanceTypeFieldConfiguration> GetSingleOrThrowAsync(
        Guid companyId,
        Guid remittanceTypeId,
        Guid id,
        bool trackChanges,
        CancellationToken ct = default
    )
    {
        var entity = await _configurationRepository.SingleOrDefaultAsync(
            e =>
                e.RemittanceTypeConfiguration.CompanyId == companyId &&
                e.RemittanceTypeConfiguration.RemittanceTypeId == remittanceTypeId &&
                e.Id == id,
            trackChanges,
            ct
        );

        return entity ?? throw new RemittanceTypeFieldConfigurationNotFoundException();
    }

    private RemittanceTypeFieldConfigurationDto Localize(RemittanceTypeFieldConfigurationDto dto)
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
