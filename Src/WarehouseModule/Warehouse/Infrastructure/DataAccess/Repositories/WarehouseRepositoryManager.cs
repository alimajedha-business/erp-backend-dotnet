using Common.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;

namespace Warehouse.Infrastructure.DataAccess.Repositories
{
    public sealed class WarehouseRepositoryManager : IWarehouseRepositoryManager
    {
        private readonly IMainDbContext _repositoryContext;
        private readonly Lazy<IWarehouseTypeRepository> _warehouseTypeRepository;
        private readonly Lazy<IWarehouseStockRepository> _warehouseStockRepository;
        private readonly Lazy<IProductHierarchyRepository> _productHierarchyRepository;
        private readonly Lazy<IProductCodeRepository> _productCodeRepository;
        public WarehouseRepositoryManager(IMainDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _warehouseTypeRepository = new Lazy<IWarehouseTypeRepository>(() =>
            new WarehouseTypeRepository(repositoryContext));

            _warehouseStockRepository = new Lazy<IWarehouseStockRepository>(() =>
               new WarehouseStockRepository(repositoryContext));

            _productHierarchyRepository = new Lazy<IProductHierarchyRepository>(() =>
               new ProductHierarchyRepository(repositoryContext));

            _productCodeRepository = new Lazy<IProductCodeRepository>(() =>
               new ProductCodeRepository(repositoryContext));
        }
        public IWarehouseTypeRepository WarehouseType => _warehouseTypeRepository.Value;

        public IWarehouseStockRepository WarehouseStock => _warehouseStockRepository.Value;

        public IProductHierarchyRepository ProductHierarchy => _productHierarchyRepository.Value;

        public IProductCodeRepository ProductCode => _productCodeRepository.Value;
        public void Save() => _repositoryContext.SaveChangesAsync();

    }
}