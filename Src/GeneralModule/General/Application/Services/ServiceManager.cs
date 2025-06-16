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
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICountryService> _countryService;
        //private readonly Lazy<IAccountSetService> _accountSetService;

        public ServiceManager(IGeneralRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper)
        {
            _countryService = new Lazy<ICountryService>(() =>
            new CountryService(repositoryManager, logger, mapper));
        //    _accountSetService = new Lazy<IAccountSetService>(() =>
        //    new AccountSetService(repositoryManager, logger, mapper));
        }
        public ICountryService CountryService => _countryService.Value;
        //public IAccountSetService AccountSetService => _accountSetService.Value;
    }
}
