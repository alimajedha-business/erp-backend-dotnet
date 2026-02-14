using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.General.Service.DTOs;
using NGErp.General.Service.Repository.Contracts;
using NGErp.General.Service.Resources;

namespace NGErp.General.Service.Services;

public class CompanyService(
    ICompanyRepository repository,
    IMapper mapper,
    IStringLocalizer<GeneralResource> stringLocalizer
) : ICompanyService
{
    private readonly ICompanyRepository _repository = repository;
    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _stringLocalizer = stringLocalizer;

    public async Task<CompanyDto> GetCompanyByIdAsync(Guid companyId, CancellationToken ct)
    {
        var company = await _repository
            .Find(e => e.Id == companyId)
            .AsNoTracking()
            .FirstOrDefaultAsync(ct) ?? throw new NotFoundException(_stringLocalizer["Company"]);

        return _mapper.Map<CompanyDto>(company);
    }
}
