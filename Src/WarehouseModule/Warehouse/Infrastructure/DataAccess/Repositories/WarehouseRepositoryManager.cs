using Warehouse.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Infrastructure.DataAccess.Repositories
{
    public sealed class WarehouseRepositoryManager : IWarehouseRepositoryManager
    {
        private readonly WarehouseDbContext _repositoryContext;
        private readonly Lazy<IWarehouseTypeRepository> _warehouseTypeRepository;
        private readonly Lazy<IWarehouseStockRepository> _warehouseStockRepository;
        public WarehouseRepositoryManager(WarehouseDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _warehouseTypeRepository = new Lazy<IWarehouseTypeRepository>(() =>
            new WarehouseTypeRepository(repositoryContext));

            _warehouseStockRepository = new Lazy<IWarehouseStockRepository>(() =>
               new WarehouseStockRepository(repositoryContext));

        }
        public IWarehouseTypeRepository WarehouseType => _warehouseTypeRepository.Value;

        public IWarehouseStockRepository WarehouseStock => _warehouseStockRepository.Value;


        public void Save() => _repositoryContext.SaveChanges();

    }
}