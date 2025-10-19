using AutoMapper;
using Common.Infrastructure.Logging;
using General.Application.Interfaces.Repositories;
using General.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Application.Services
{
    internal sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;
        private readonly Lazy<IDomainService> _domainService;
        private readonly Lazy<ICompanyService> _companyService;


        public ServiceManager(IGeneralRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper)
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
