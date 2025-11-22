using AutoMapper;
using Base.Infrastructure.Logging;
using General.Service.Interfaces.Repositories;
using General.Service.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Service.Services
{
    internal sealed class GeneralServiceManager : IGeneralServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;
        private readonly Lazy<IDomainService> _domainService;
        private readonly Lazy<ICompanyService> _companyService;


        public GeneralServiceManager(IGeneralRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper)
        {
            _countryService = new Lazy<ICountryService>(() =>
            new CountryService(repositoryManager, logger, mapper));

            _domainService = new Lazy<IDomainService>(() =>
            new DomainService(repositoryManager, logger, mapper));

            _companyService = new Lazy<ICompanyService>(() =>
            new CompanyService(repositoryManager, logger, mapper));
        }
        public ICountryService CountryService => _countryService.Value;

        public IDomainService DomainService => _domainService.Value;

        public ICompanyService CompanyService => _companyService.Value;

    }
}
