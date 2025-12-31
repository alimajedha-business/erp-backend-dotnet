using AutoMapper;
using Microsoft.Extensions.Logging;
using NGErp.Base.Infrastructure.Logging;
using NGErp.General.Domain.Entities;
using NGErp.General.Domain.Exceptions;
using NGErp.General.Service.DTOs;
using NGErp.General.Service.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDomainRepository _domainRepository;
        private readonly ILogger<Company> _logger;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, ILogger<Company> logger, IMapper mapper,
            IDomainRepository domainRepository)
        {
            _companyRepository = companyRepository;
            _logger = logger;
            _mapper = mapper;
            _domainRepository = domainRepository;
        }

        public async Task<CompanyDto?> GetCompanyAsync(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task<CompanyDto?> GetCompanyForDomainAsync(Guid domainId, Guid companyId)
        {
            var domain = await _domainRepository.GetByIdAsync(domainId);
            if (domain == null)
                throw new DomainNotFoundException(domainId);

            var company = await _companyRepository.GetCompanyForDomainAsync(domainId, companyId);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
    }
}
