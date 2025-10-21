using AutoMapper;
using Common.Infrastructure.Logging;
using General.Application.Interfaces.Services;
using HCM.Application.Interfaces.Repositories;
using HCM.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Application.Services
{
    internal sealed class HCMServiceManager : IHCMServiceManager
    {
        private readonly Lazy<IDepartmentService> _departmentService;

        public HCMServiceManager(IHCMRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper,
            IGeneralServiceManager generalServiceManager)
        {
            _departmentService = new Lazy<IDepartmentService>(() =>
            new DepartmentService(repositoryManager, logger, mapper, generalServiceManager));

            //    _accountSetService = new Lazy<IAccountSetService>(() =>
            //    new AccountSetService(repositoryManager, logger, mapper));
        }

        public IDepartmentService DepartmentService => _departmentService.Value;

    }
}
