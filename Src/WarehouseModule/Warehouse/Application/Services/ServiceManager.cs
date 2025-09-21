using AutoMapper;
using Common.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Interfaces.Services;

namespace Warehouse.Application.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IWarehouseTypeService> _WarehouseTypeService;
        private readonly Lazy<IWarehouseStockService> _WarehouseStockService;


        public ServiceManager(IWarehouseRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper)
        {
            _WarehouseTypeService = new Lazy<IWarehouseTypeService>(() =>
            new WarehouseTypeService(repositoryManager, logger, mapper));

            _WarehouseStockService = new Lazy<IWarehouseStockService>(() =>
            new WarehouseStockService(repositoryManager, logger, mapper));

        }
        public IWarehouseTypeService WarehouseTypeService => _WarehouseTypeService.Value;


        public IWarehouseStockService WarehouseStockService => _WarehouseStockService.Value;

    }
}
