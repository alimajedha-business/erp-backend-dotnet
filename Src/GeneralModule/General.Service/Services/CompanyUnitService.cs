using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.General.Service.DTOs;
using NGErp.General.Service.Repository.Contracts;
using NGErp.General.Service.Resources;

namespace NGErp.General.Service.Services;

public class CompanyUnitService(
    ICompanyUnitRepository repository,
    ICompanyService companyService,
    IMapper mapper,
    IStringLocalizer<GeneralResource> localizer
) : ICompanyUnitService
{
    private readonly ICompanyUnitRepository _repository = repository;
    private readonly ICompanyService _companyService = companyService;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;

    public async Task<CompanyUnitDto> GetByIdAsync(
        Guid companyId,
        Guid companyUnitId,
        CancellationToken ct
    )
    {
        await _companyService.GetByIdAsync(companyId, ct);

        var companyUnit = await _repository
            .Find(e => e.Id == companyUnitId)
            .AsNoTracking()
            .FirstOrDefaultAsync(ct) ?? throw new NotFoundException(_localizer["CompanyUnit"]);

        return _mapper.Map<CompanyUnitDto>(companyUnit);
    }
}
