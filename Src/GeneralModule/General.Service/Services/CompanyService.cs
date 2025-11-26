using AutoMapper;
using NGErp.Base.Infrastructure.Logging;
using NGErp.General.Service.DTOs;
using NGErp.General.Service.Interfaces.Repositories;
using NGErp.General.Service.Interfaces.Services;
using NGErp.General.Domain.Entities;
using NGErp.General.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IGeneralRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CompanyService(IGeneralRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyDto?> GetCompanyAsync(int companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        public async Task<CompanyDto?> GetCompanyForDomainAsync(int domainId, int companyId, bool trackChanges)
        {
            var domain = await _repository.Domain.GetDomainAsync(domainId, trackChanges);
            if (domain == null)
                throw new DomainNotFoundException(domainId);

            var company = await _repository.Company.GetCompanyForDomainAsync(domainId, companyId, trackChanges);
            if (company == null)
                throw new CompanyNotFoundException(companyId);

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }
    }
}
