using Warehouse.Application.DTOs;
using Warehouse.Domain.Exceptions;
using Warehouse.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Common.Domain.Exceptions;
using Warehouse.Application.Interfaces.Services;
using Warehouse.Application.Interfaces.Repositories;
using Common.Infrastructure.Logging;


namespace Warehouse.Application.Services
{
    internal sealed class ProductHierarchyService : Warehouse.Interfaces.Services.IProductHierarchyService
    {
        private readonly IWarehouseRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

    

        public ProductHierarchyService(IWarehouseRepositoryManager repository, ILoggerService logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public ProductHierarchyDto Create(ProductHierarchyForCreationDto ProductHierarchy)
        {
            var ProductHierarchyEntity = _mapper.Map<ProductHierarchy>(ProductHierarchy);
            _repository.ProductHierarchy.Create(ProductHierarchyEntity);
            _repository.Save();
            var ProductHierarchyToReturn = _mapper.Map<ProductHierarchyDto>(ProductHierarchyEntity);
            return ProductHierarchyToReturn;
        }

        public (IEnumerable<ProductHierarchyDto> ProductHierarchies, string CompanyIds) CreateCollection(IEnumerable<ProductHierarchyForCreationDto> ProductHierarchyCollection)
        {
            if (ProductHierarchyCollection is null)
                throw new ProductHierarchyCollectionBadRequestException();
            var ProductHierarchyEntities = _mapper.Map<IEnumerable<ProductHierarchy>>(ProductHierarchyCollection);
            foreach (var ProductHierarchy in ProductHierarchyEntities)
            {
                _repository.ProductHierarchy.Create(ProductHierarchy);
            }
            _repository.Save();
            var ProductHierarchyCollectionToReturn =
            _mapper.Map<IEnumerable<ProductHierarchyDto>>(ProductHierarchyEntities);
            var ids = string.Join(",", ProductHierarchyCollectionToReturn.Select(c => c.Id));
            return (ProductHierarchies: ProductHierarchyCollectionToReturn, ids);
        }

        public void Delete(int CompanyId, bool trackChanges)
        {
            var ProductHierarchy = _repository.ProductHierarchy.Get(CompanyId, trackChanges);
            if (ProductHierarchy is null)
                throw new ProductHierarchyNotFoundException(CompanyId);
            _repository.ProductHierarchy.Delete(ProductHierarchy);
            _repository.Save();
        }

        public IEnumerable<ProductHierarchyDto> GetAll(bool trackChanges)
        {
            var ProductHierarchies = _repository.ProductHierarchy.GetAll(trackChanges);
            var ProductHierarchiesDto = _mapper.Map<IEnumerable<ProductHierarchyDto>>(ProductHierarchies);
            return ProductHierarchiesDto;
        }

        public IEnumerable<ProductHierarchyDto> GetByIds(IEnumerable<int> CompanyIds, bool trackChanges)
        {
            if (CompanyIds is null)
                throw new IdParametersBadRequestException();
            var ProductHierarchyEntities = _repository.ProductHierarchy.GetByIds(CompanyIds, trackChanges);
            if (CompanyIds.Count() != ProductHierarchyEntities.Count())
                throw new CollectionByIdsBadRequestException();
            var ProductHierarchiesToReturn = _mapper.Map<IEnumerable<ProductHierarchyDto>>(ProductHierarchyEntities);
            return ProductHierarchiesToReturn;
        }

        public ProductHierarchyDto? Get(int CompanyId, bool trackChanges)
        {
            var ProductHierarchy = _repository.ProductHierarchy.Get(CompanyId, trackChanges);
            if (ProductHierarchy is null)
                throw new ProductHierarchyNotFoundException(CompanyId);
            var ProductHierarchyDto = _mapper.Map<ProductHierarchyDto>(ProductHierarchy);
            return ProductHierarchyDto;
        }

        public void Update(int CompanyId, ProductHierarchyForUpdateDto ProductHierarchy, bool trackChanges)
        {
            var ProductHierarchyEntity = _repository.ProductHierarchy.Get(CompanyId, trackChanges);
            if (ProductHierarchyEntity is null)
                throw new ProductHierarchyNotFoundException(CompanyId);
            _mapper.Map(ProductHierarchy, ProductHierarchyEntity);
            _repository.Save();
        }

        public (ProductHierarchyForUpdateDto ProductHierarchyForUpdate, ProductHierarchy ProductHierarchyEntity) GetProductHierarchyForPatch(int CompanyId, bool trackChanges)
        {
            var ProductHierarchyEntity = _repository.ProductHierarchy.Get(CompanyId, trackChanges);
            if (ProductHierarchyEntity is null)
                throw new ProductHierarchyNotFoundException(CompanyId);
            var ProductHierarchyToPatch = _mapper.Map<ProductHierarchyForUpdateDto>(ProductHierarchyEntity);
            return (ProductHierarchyToPatch, ProductHierarchyEntity);
        }

        public void SaveChangesForPatch(ProductHierarchyForUpdateDto ProductHierarchyToPatch, ProductHierarchy ProductHierarchyEntity)
        {
            _mapper.Map(ProductHierarchyToPatch, ProductHierarchyEntity);
            _repository.Save();
        }
    }
}
